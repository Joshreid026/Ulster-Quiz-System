using System.Reflection;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class CategoryTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Category> category;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Category.ResetCategoryNextIDCounter();

        category = new List<Category>();
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void CategoryDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        category.Add(new Category());

        // Assert
        Assert.AreEqual(categoryID, category[0].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, category[0].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, category[0].CategoryDescription, "CategoryDescription was not initialized correctly.");
    }

    [TestMethod]
    public void CategoryParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        category.Add(new Category(categoryName, categoryDescription));

        // Assert
        Assert.AreEqual(categoryID, category[0].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, category[0].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, category[0].CategoryDescription, "CategoryDescription was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void CategoryID_ShouldSetCorrectly()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        category.Add(new Category(categoryName, categoryDescription));

        // Assert
        Assert.AreEqual(categoryID, category[0].CategoryID, "CategoryID was not set correctly.");
    }

    [TestMethod]
    public void CategoryID_Setter_ShouldBePrivate()
    {
        // Arrange
        PropertyInfo property = typeof(Category).GetProperty("CategoryID");

        // Act
        MethodInfo setter = property.SetMethod;

        // Assert
        Assert.IsTrue(setter.IsPrivate, "CategoryID setter should be private.");
    }

    [TestMethod]
    public void CategoryProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string categoryName = "Defaulted";
        string categoryDescription = "Defaulted Description";

        // Act
        category.Add(new Category());

        category[0].CategoryName = categoryName;
        category[0].CategoryDescription = categoryDescription;

        // Assert
        Assert.AreEqual(categoryName, category[0].CategoryName, "CategoryName was not set correctly.");
        Assert.AreEqual(categoryDescription, category[0].CategoryDescription, "CategoryDescription was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
