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
        Assert.AreEqual(categoryID, _category[0].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, _category[0].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, _category[0].CategoryDescription, "CategoryDescription was not initialized correctly.");
    }

    [TestMethod]
    public void CategoryParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        _category.Add(new Category(categoryID, categoryName, categoryDescription));

        // Assert
        Assert.AreEqual(categoryID, _category[1].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, _category[1].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, _category[1].CategoryDescription, "CategoryDescription was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
}
