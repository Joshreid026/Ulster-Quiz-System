using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class CategoryTests
{
    [TestMethod]
    public void CategoryDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 0;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        Category category = new Category();

        // Assert
        Assert.AreEqual(categoryID, category.CategoryID);
        Assert.AreEqual(categoryName, category.CategoryName);
        Assert.AreEqual(categoryDescription, category.CategoryDescription);
    }

    [TestMethod]
    public void AdminParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int categoryID = 0;
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        Category category = new Category(categoryID, categoryName, categoryDescription);

        // Assert
        Assert.AreEqual(categoryID, category.CategoryID);
        Assert.AreEqual(categoryName, category.CategoryName);
        Assert.AreEqual(categoryDescription, category.CategoryDescription);
    }
}
