# LeetCode C# Solutions

Bu repo C# ile yazılmış LeetCode çözümlerini içerir.

## Yapı

Her problem klasörü ayrı bir C# class library projesidir. Bu sayede LeetCode'un beklediği gibi her dosyada namespace kullanmadan `public class Solution` yazılabilir.

## Derleme

Tüm C# çözümlerini derlemek için:

```powershell
dotnet build "C#\C#.csproj"
```

Tek bir problemi derlemek için:

```powershell
dotnet build "C#\2812. Find the Safest Path in a Grid\P2812.FindTheSafestPathInAGrid.csproj"
```

## Yeni Çözüm Ekleme Akışı

1. `C#` altında yeni problem klasörü oluştur.
2. İçine `Solution.cs` dosyasını ekle ve LeetCode formatında `public class Solution` kullan.
3. Aynı klasöre küçük bir `.csproj` dosyası ekle.
4. `dotnet build "C#\C#.csproj"` ile kontrol et.
5. Değişiklikleri commit'le:

```powershell
git status
git add .
git commit -m "Add <problem-name> solution"
git push
```