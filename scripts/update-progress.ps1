param(
    [string]$Root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
)

$ErrorActionPreference = 'Stop'
$utf8NoBom = New-Object System.Text.UTF8Encoding $false

function Normalize-Key([string]$Value) {
    if ([string]::IsNullOrWhiteSpace($Value)) { return '' }
    return ($Value.ToLowerInvariant() -replace '[^a-z0-9]+', '')
}

function Escape-Xml([string]$Value) {
    if ($null -eq $Value) { return '' }
    return [System.Security.SecurityElement]::Escape($Value)
}

function Test-IgnoredPath([string]$Path, [string]$LanguageRoot) {
    $relative = $Path.Substring($LanguageRoot.Length).TrimStart([char[]]'\/')
    $segments = $relative -split '[\\/]'
    $ignored = @('bin', 'obj', 'build', 'target', 'out', '.gradle', '.idea', '.vscode')
    foreach ($segment in $segments) {
        if ($ignored -contains $segment) { return $true }
    }
    return $false
}

function Get-ProblemInfo($File, [string]$LanguageRoot) {
    $relative = $File.FullName.Substring($LanguageRoot.Length).TrimStart([char[]]'\/')
    $folderName = Split-Path -Leaf (Split-Path -Parent $File.FullName)

    if ($File.BaseName -ieq 'Solution') {
        $name = $folderName
    }
    else {
        $name = $File.BaseName
    }

    $id = $null
    if ($name -match '^\s*(\d{1,5})[\.\s_-]+(.+)$') {
        $id = [int]$Matches[1]
        $title = $Matches[2].Trim()
    }
    elseif ($relative -match '(^|[\\/])(\d{1,5})[\.\s_-]+([^\\/]+)') {
        $id = [int]$Matches[2]
        $title = $Matches[3].Trim()
    }
    else {
        $title = $name.Trim()
    }

    [PSCustomObject]@{
        Id = $id
        Title = $title
        SourceName = $name
        RelativePath = $relative
    }
}

function Find-Metadata($Metadata, $ProblemInfo) {
    foreach ($item in @($Metadata)) {
        if ($null -ne $ProblemInfo.Id -and $null -ne $item.id -and [int]$item.id -eq [int]$ProblemInfo.Id) {
            return $item
        }
    }

    $problemTitle = Normalize-Key $ProblemInfo.Title
    foreach ($item in @($Metadata)) {
        if ($item.title -and (Normalize-Key $item.title) -eq $problemTitle) {
            return $item
        }
    }

    return $null
}

function Add-SvgLine([System.Collections.Generic.List[string]]$Svg, [string]$Line) {
    $Svg.Add($Line) | Out-Null
}

function Add-SvgText([System.Collections.Generic.List[string]]$Svg, [int]$X, [int]$Y, [string]$Fill, [int]$Size, [string]$Text, [string]$Weight = '400', [string]$Anchor = '') {
    $anchorPart = ''
    if (-not [string]::IsNullOrWhiteSpace($Anchor)) { $anchorPart = ' text-anchor="' + $Anchor + '"' }
    Add-SvgLine $Svg ('  <text x="{0}" y="{1}"{2} fill="{3}" font-family="Segoe UI, Arial, sans-serif" font-size="{4}" font-weight="{5}">{6}</text>' -f $X, $Y, $anchorPart, $Fill, $Size, $Weight, (Escape-Xml $Text))
}

function Add-SvgRect([System.Collections.Generic.List[string]]$Svg, [int]$X, [int]$Y, [int]$Width, [int]$Height, [int]$Rx, [string]$Fill, [string]$Stroke = '') {
    $strokePart = ''
    if (-not [string]::IsNullOrWhiteSpace($Stroke)) { $strokePart = ' stroke="' + $Stroke + '"' }
    Add-SvgLine $Svg ('  <rect x="{0}" y="{1}" width="{2}" height="{3}" rx="{4}" fill="{5}"{6}/>' -f $X, $Y, $Width, $Height, $Rx, $Fill, $strokePart)
}

function Get-DifficultyColor([string]$Difficulty) {
    switch ($Difficulty) {
        'Easy' { '#22c55e' }
        'Medium' { '#f59e0b' }
        'Hard' { '#ef4444' }
        default { '#94a3b8' }
    }
}

$metadataPath = Join-Path $Root 'metadata\problems.json'
$metadata = @()
if (Test-Path -LiteralPath $metadataPath) {
    $metadata = Get-Content -LiteralPath $metadataPath -Raw -Encoding UTF8 | ConvertFrom-Json
}

$languages = @(
    [PSCustomObject]@{ Name = 'C#'; Folder = 'C#'; Pattern = 'Solution.cs' },
    [PSCustomObject]@{ Name = 'Java'; Folder = 'Java'; Pattern = '*.java' }
)

$problems = [ordered]@{}

foreach ($language in $languages) {
    $languageRoot = Join-Path $Root $language.Folder
    if (-not (Test-Path -LiteralPath $languageRoot)) { continue }

    $files = Get-ChildItem -LiteralPath $languageRoot -Recurse -File -Filter $language.Pattern -ErrorAction SilentlyContinue |
        Where-Object { -not (Test-IgnoredPath $_.FullName $languageRoot) }

    foreach ($file in $files) {
        $info = Get-ProblemInfo $file $languageRoot
        if ([string]::IsNullOrWhiteSpace($info.Title)) { continue }

        $metadataItem = Find-Metadata $metadata $info
        if ($metadataItem) {
            $difficulty = [string]$metadataItem.difficulty
            $title = [string]$metadataItem.title
            $id = if ($metadataItem.id) { [int]$metadataItem.id } else { $info.Id }
        }
        else {
            $difficulty = 'Unknown'
            $title = $info.Title
            $id = $info.Id
        }

        if ($difficulty -eq 'Med') { $difficulty = 'Medium' }
        if (@('Easy', 'Medium', 'Hard') -notcontains $difficulty) { $difficulty = 'Unknown' }

        $key = if ($null -ne $id) { "id:$id" } else { "title:$(Normalize-Key $title)" }
        if (-not $problems.Contains($key)) {
            $problems[$key] = [PSCustomObject]@{
                Id = $id
                Title = $title
                Difficulty = $difficulty
                Languages = New-Object System.Collections.Generic.List[string]
            }
        }

        if (-not $problems[$key].Languages.Contains($language.Name)) {
            $problems[$key].Languages.Add($language.Name)
        }
    }
}

$records = @($problems.Values)
$total = $records.Count
$difficultyCounts = [ordered]@{ Easy = 0; Medium = 0; Hard = 0; Unknown = 0 }
foreach ($record in $records) { $difficultyCounts[$record.Difficulty]++ }

$languageCounts = [ordered]@{}
foreach ($language in $languages) { $languageCounts[$language.Name] = 0 }
foreach ($record in $records) {
    foreach ($languageName in $record.Languages) {
        if (-not $languageCounts.Contains($languageName)) { $languageCounts[$languageName] = 0 }
        $languageCounts[$languageName]++
    }
}

$generatedAt = Get-Date -Format 'yyyy-MM-dd HH:mm'

$markdownLines = New-Object System.Collections.Generic.List[string]
$markdownLines.Add('<!-- LEETCODE_PROGRESS_START -->') | Out-Null
$markdownLines.Add('## Progress') | Out-Null
$markdownLines.Add('') | Out-Null
$markdownLines.Add('![LeetCode progress](assets/progress.svg)') | Out-Null
$markdownLines.Add('') | Out-Null
$markdownLines.Add("_Generated by `scripts/update-progress.ps1` on $generatedAt._") | Out-Null
$markdownLines.Add('') | Out-Null
$markdownLines.Add('| Metric | Count |') | Out-Null
$markdownLines.Add('| --- | ---: |') | Out-Null
$markdownLines.Add("| Total solved | $total |") | Out-Null
$markdownLines.Add("| Easy | $($difficultyCounts.Easy) |") | Out-Null
$markdownLines.Add("| Medium | $($difficultyCounts.Medium) |") | Out-Null
$markdownLines.Add("| Hard | $($difficultyCounts.Hard) |") | Out-Null
if ($difficultyCounts.Unknown -gt 0) {
    $markdownLines.Add("| Unknown difficulty | $($difficultyCounts.Unknown) |") | Out-Null
}
$markdownLines.Add('') | Out-Null
$markdownLines.Add('| Language | Problems |') | Out-Null
$markdownLines.Add('| --- | ---: |') | Out-Null
foreach ($languageName in $languageCounts.Keys) {
    $markdownLines.Add("| $languageName | $($languageCounts[$languageName]) |") | Out-Null
}
$markdownLines.Add('<!-- LEETCODE_PROGRESS_END -->') | Out-Null
$progressBlock = ($markdownLines -join [Environment]::NewLine)

$readmePath = Join-Path $Root 'README.md'
$readme = if (Test-Path -LiteralPath $readmePath) { Get-Content -LiteralPath $readmePath -Raw -Encoding UTF8 } else { '# LeetCode Solutions' + [Environment]::NewLine }
$startMarker = '<!-- LEETCODE_PROGRESS_START -->'
$endMarker = '<!-- LEETCODE_PROGRESS_END -->'
$startIndex = $readme.IndexOf($startMarker)
$endIndex = $readme.IndexOf($endMarker)
if ($startIndex -ge 0 -and $endIndex -ge $startIndex) {
    $readme = $readme.Substring(0, $startIndex) + $progressBlock + $readme.Substring($endIndex + $endMarker.Length)
}
else {
    $readme = $readme.TrimEnd() + [Environment]::NewLine + [Environment]::NewLine + $progressBlock + [Environment]::NewLine
}
[System.IO.File]::WriteAllText($readmePath, $readme, $utf8NoBom)

$assetsDir = Join-Path $Root 'assets'
New-Item -ItemType Directory -Force -Path $assetsDir | Out-Null
$svgPath = Join-Path $assetsDir 'progress.svg'

function Get-BarWidth([int]$Count, [int]$MaxWidth) {
    if ($total -le 0) { return 0 }
    [Math]::Round(($Count / [double]$total) * $MaxWidth)
}

$easyWidth = Get-BarWidth $difficultyCounts.Easy 420
$mediumWidth = Get-BarWidth $difficultyCounts.Medium 420
$hardWidth = Get-BarWidth $difficultyCounts.Hard 420
$unknownWidth = Get-BarWidth $difficultyCounts.Unknown 420

$svg = New-Object System.Collections.Generic.List[string]
Add-SvgLine $svg '<svg width="760" height="310" viewBox="0 0 760 310" fill="none" xmlns="http://www.w3.org/2000/svg" role="img" aria-labelledby="title desc">'
Add-SvgLine $svg '  <title id="title">LeetCode progress</title>'
Add-SvgLine $svg '  <desc id="desc">Solved problem counts by difficulty and language.</desc>'
Add-SvgLine $svg '  <rect width="760" height="310" rx="22" fill="#202020"/>'
Add-SvgLine $svg '  <rect x="1" y="1" width="758" height="308" rx="21" stroke="#3f3f46"/>'
Add-SvgText $svg 28 42 '#a3a3a3' 18 'Total Solved' '600'
Add-SvgText $svg 28 86 '#0ea5e9' 42 ([string]$total) '700'
Add-SvgText $svg 100 82 '#38bdf8' 16 'Problems'

$chipY = 112
$chipData = @(
    [PSCustomObject]@{ X = 28; Label = 'Easy'; Count = $difficultyCounts.Easy; Color = '#22c55e' },
    [PSCustomObject]@{ X = 152; Label = 'Med.'; Count = $difficultyCounts.Medium; Color = '#f59e0b' },
    [PSCustomObject]@{ X = 276; Label = 'Hard'; Count = $difficultyCounts.Hard; Color = '#ef4444' }
)
if ($difficultyCounts.Unknown -gt 0) {
    $chipData += [PSCustomObject]@{ X = 400; Label = 'Unknown'; Count = $difficultyCounts.Unknown; Color = '#94a3b8' }
}
foreach ($chip in $chipData) {
    Add-SvgRect $svg $chip.X $chipY 104 38 10 '#3a3a3a'
    Add-SvgText $svg ($chip.X + 16) ($chipY + 25) $chip.Color 14 $chip.Label '700'
    Add-SvgText $svg ($chip.X + 74) ($chipY + 25) '#f4f4f5' 15 ([string]$chip.Count) '700'
}

$barX = 28
$barY = 182
Add-SvgText $svg 28 174 '#a3a3a3' 15 'Difficulty Mix' '600'
Add-SvgRect $svg $barX $barY 420 16 8 '#3f3f46'
$currentX = $barX
foreach ($part in @(
    [PSCustomObject]@{ Width = $easyWidth; Color = '#22c55e' },
    [PSCustomObject]@{ Width = $mediumWidth; Color = '#f59e0b' },
    [PSCustomObject]@{ Width = $hardWidth; Color = '#ef4444' },
    [PSCustomObject]@{ Width = $unknownWidth; Color = '#94a3b8' }
)) {
    if ($part.Width -gt 0) {
        Add-SvgRect $svg $currentX $barY $part.Width 16 8 $part.Color
        $currentX += $part.Width
    }
}

$languageY = 230
Add-SvgText $svg 28 222 '#a3a3a3' 15 'Languages' '600'
foreach ($languageName in $languageCounts.Keys) {
    $count = $languageCounts[$languageName]
    $width = if ($total -le 0 -or $count -le 0) { 0 } else { [Math]::Max(4, [Math]::Round(($count / [double]$total) * 260)) }
    Add-SvgText $svg 28 ($languageY + 13) '#e4e4e7' 14 $languageName
    Add-SvgRect $svg 92 $languageY 260 14 7 '#3f3f46'
    if ($width -gt 0) { Add-SvgRect $svg 92 $languageY $width 14 7 '#8b5cf6' }
    Add-SvgText $svg 366 ($languageY + 13) '#a3a3a3' 13 ([string]$count)
    $languageY += 26
}

$bubbleRecords = @($records | Sort-Object @{Expression = { if ($null -eq $_.Id) { 999999 } else { $_.Id } }}, Title | Select-Object -First 12)
Add-SvgText $svg 500 42 '#a3a3a3' 15 'Problem Map' '600'
for ($i = 0; $i -lt $bubbleRecords.Count; $i++) {
    $record = $bubbleRecords[$i]
    $x = 512 + (($i % 4) * 54)
    $y = 82 + ([Math]::Floor($i / 4) * 56)
    $color = Get-DifficultyColor $record.Difficulty
    $label = if ($record.Id) { [string]$record.Id } else { $record.Title }
    if ($label.Length -gt 9) { $label = $label.Substring(0, 9) }
    Add-SvgLine $svg ('  <circle cx="{0}" cy="{1}" r="23" fill="#3f3f46" stroke="{2}" stroke-width="2"/>' -f $x, $y, $color)
    Add-SvgText $svg $x ($y + 4) $color 10 $label '700' 'middle'
}

Add-SvgText $svg 28 292 '#71717a' 12 "Updated $generatedAt"
Add-SvgLine $svg '</svg>'
[System.IO.File]::WriteAllText($svgPath, ($svg -join [Environment]::NewLine), $utf8NoBom)

Write-Host "Updated README progress for $total problems."