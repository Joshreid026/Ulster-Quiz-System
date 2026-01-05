using System.Reflection;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class CategoryTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Category> categories;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Category.ResetCategoryNextIDCounter();

        categories = new List<Category>();
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
        categories.Add(new Category());

        // Assert
        Assert.AreEqual(categoryID, categories[0].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, categories[0].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, categories[0].CategoryDescription, "CategoryDescription was not initialized correctly.");
    }

    [TestMethod]
    public void CategoryParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 1;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        categories.Add(new Category(categoryName, categoryDescription));

        // Assert
        Assert.AreEqual(categoryID, categories[0].CategoryID, "CategoryID was not initialized correctly.");
        Assert.AreEqual(categoryName, categories[0].CategoryName, "CategoryName was not initialized correctly.");
        Assert.AreEqual(categoryDescription, categories[0].CategoryDescription, "CategoryDescription was not initialized correctly.");
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
        categories.Add(new Category(categoryName, categoryDescription));

        // Assert
        Assert.AreEqual(categoryID, categories[0].CategoryID, "CategoryID was not set correctly.");
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
        categories.Add(new Category());

        categories[0].CategoryName = categoryName;
        categories[0].CategoryDescription = categoryDescription;

        // Assert
        Assert.AreEqual(categoryName, categories[0].CategoryName, "CategoryName was not set correctly.");
        Assert.AreEqual(categoryDescription, categories[0].CategoryDescription, "CategoryDescription was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
    [TestMethod]
    public void ResetCategoryNextIDCounter_ShouldResetAdminIdToOne()
    {
        // Arrange
        categories.AddRange(new[]
        {
            new Category("Programming", "Concepts of object-oriented programming and coding principles"),
            new Category("Data Structures", "Arrays, lists, stacks, queues, trees, and their applications"),
        });

        // Act
        Category.ResetCategoryNextIDCounter();
        categories.Add(new Category("Software Design", "Design patterns, architecture principles, and system modelling"));

        // Assert
        Assert.AreEqual(1, categories[2].CategoryID, "ResetCategoryNextIDCounter did not reset CategoryID.");
    }
}
