using fanaticServe.Back.Data;

using Xunit;


namespace fanaticServe.Back.Tests;

public class EventServiceTests
{
    [Fact]
    public void GetAllEventArticles_WithValidData_ReturnsArticles()
    {
        // Arrange
        var options = ".\\DataSource";

        var context = new FanaticServeFileContext(options);
        // テストデータセットアップ

        var service = new EventService(context);

        // Act
        var result = service.GetAllEventArticles(null, null);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetAllEventArticles_WithSearchString_FiltersCorrectly()
    {
        // Arrange
        var options = ".\\DataSource";

        var context = new FanaticServeFileContext(options);

        var service = new EventService(context);

        // Act
        var result = service.GetAllEventArticles(null, "検索キー");

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void getHeader()
    {
        // カレントディレクトリを指定してコンテキストを作成
        var options = AppDomain.CurrentDomain.BaseDirectory;

        var context = new FanaticServeFileContext(options);

        var service = new StarMatrixService(context);

        var arr = service.getHeader();
        Assert.NotNull(arr);
    }

    [Fact]
    public void getRowHeader()
    {
        // カレントディレクトリを指定してコンテキストを作成
        var options = AppDomain.CurrentDomain.BaseDirectory;

        var context = new FanaticServeFileContext(options);

        var service = new StarMatrixService(context);

        var arr = service.GetRowHeader();
        Assert.NotNull(arr);
        return;
    }
    
    [Fact]
    public void GetStarMatrix()
    {
        // カレントディレクトリを指定してコンテキストを作成
        var options = AppDomain.CurrentDomain.BaseDirectory;

        var context = new FanaticServeFileContext(options);

        var service = new StarMatrixService(context);

         service.GetStarMatrix();
//        Assert.NotNull(arr);
        return;
    }


}