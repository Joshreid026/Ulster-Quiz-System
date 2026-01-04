using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class CategoryTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Category> _category;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _category = new List<Category>();
        _category.Add(new Category());
        _category.Add(new Category(1, "Default", "Default Description"));
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void CategoryDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 0;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Assert
        Assert.AreEqual(categoryID, _category[0].CategoryID);
        Assert.AreEqual(categoryName, _category[0].CategoryName);
        Assert.AreEqual(categoryDescription, _category[0].CategoryDescription);
    }

    [TestMethod]
    public void CategoryParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        Category category = new Category(categoryID, categoryName, categoryDescription);

        // Assert
        Assert.AreEqual(categoryID, _category[1].CategoryID);
        Assert.AreEqual(categoryName, _category[1].CategoryName);
        Assert.AreEqual(categoryDescription, _category[1].CategoryDescription);
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
}
