using Microsoft.AspNetCore.Mvc.DataAnnotations;
using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using System.Security.AccessControl;
using Xunit;

namespace ShoppingList.Tests;

/// <summary>
/// Unit tests for ShoppingListService.
///
/// IMPORTANT: Write your tests here using Test Driven Development (TDD)
///
/// TDD Workflow:
/// 1. Write a test for a specific behavior (RED - test fails)
/// 2. Implement minimal code to make the test pass (GREEN - test passes)
/// 3. Refactor the code if needed (REFACTOR - improve without changing behavior)
/// 4. Repeat for the next behavior
///
/// Test Examples:
/// - See ShoppingItemTests.cs for examples of well-structured unit tests
/// - Follow the Arrange-Act-Assert pattern
/// - Use descriptive test names: Method_Scenario_ExpectedBehavior
///
/// What to Test:
/// - Happy path scenarios (normal, expected usage)
/// - Input validation (null/empty IDs, invalid parameters)
/// - Edge cases (empty array, array expansion, last item, etc.)
/// - Array management (shifting after delete, compacting, reordering)
/// - Search functionality (case-insensitive, matching in name/notes)
///
/// Recommended Test Categories:
///
/// GetAll() tests:
/// - GetAll_WhenEmpty_ShouldReturnEmptyList
/// - GetAll_WithItems_ShouldReturnAllItems
/// - GetAll_ShouldNotReturnMoreThanActualItemCount
///
/// GetById() tests:
/// - GetById_WithValidId_ShouldReturnItem
/// - GetById_WithInvalidId_ShouldReturnNull
/// - GetById_WithNullId_ShouldReturnNull
/// - GetById_WithEmptyId_ShouldReturnNull
///


///
/// Update() tests:
/// - Update_WithValidId_ShouldUpdateAndReturnItem
/// - Update_WithInvalidId_ShouldReturnNull
/// - Update_ShouldNotChangeId
/// - Update_ShouldNotChangeIsPurchased
///
/// Delete() tests:
/// - Delete_WithValidId_ShouldReturnTrue
/// - Delete_WithInvalidId_ShouldReturnFalse
/// - Delete_ShouldRemoveItemFromList
/// - Delete_ShouldShiftRemainingItems
/// - Delete_ShouldDecrementItemCount
/// - Delete_LastItem_ShouldWork
/// - Delete_FirstItem_ShouldWork
/// - Delete_MiddleItem_ShouldWork
///
/// Search() tests:
/// - Search_WithEmptyQuery_ShouldReturnAllItems
/// - Search_WithNullQuery_ShouldReturnAllItems
/// - Search_MatchingName_ShouldReturnItem
/// - Search_MatchingNotes_ShouldReturnItem
/// - Search_ShouldBeCaseInsensitive
/// - Search_WithNoMatches_ShouldReturnEmpty
/// - Search_ShouldFindPartialMatches
///
/// ClearPurchased() tests:
/// - ClearPurchased_WithNoPurchasedItems_ShouldReturnZero
/// - ClearPurchased_ShouldRemoveOnlyPurchasedItems
/// - ClearPurchased_ShouldReturnCorrectCount
/// - ClearPurchased_ShouldShiftRemainingItems
///
/// TogglePurchased() tests:
/// - TogglePurchased_WithValidId_ShouldReturnTrue
/// - TogglePurchased_WithInvalidId_ShouldReturnFalse
/// - TogglePurchased_ShouldToggleFromFalseToTrue
/// - TogglePurchased_ShouldToggleFromTrueToFalse
///
/// Reorder() tests:
/// - Reorder_WithValidOrder_ShouldReturnTrue
/// - Reorder_WithInvalidId_ShouldReturnFalse
/// - Reorder_WithMissingIds_ShouldReturnFalse
/// - Reorder_WithDuplicateIds_ShouldReturnFalse
/// - Reorder_ShouldChangeItemOrder
/// - Reorder_WithEmptyList_ShouldReturnFalse
/// </summary>

public class ShoppingListServiceTests
{
    /// Add() tests:
    /// - Add_WithValidInput_ShouldReturnItem
    /// - Add_ShouldGenerateUniqueId
    /// - Add_ShouldIncrementItemCount
    /// - Add_WhenArrayFull_ShouldExpandArray
    /// - Add_AfterArrayExpansion_ShouldContinueWorking
    /// - Add_ShouldSetIsPurchasedToFalse
    //TODO: Write your tests here following the TDD workflow

    // Example test structure:
    [Fact]
    public void Add_WithValidInput_ShouldReturnItem()
    {
        // Arrange
        var service = new ShoppingListService();

        // Act
        var item = service.Add("Milk", 2, "Lactose-free");

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Milk", item!.Name);
        Assert.Equal(2, item.Quantity);
    }

    [Fact]
    public void Add_ShouldGenerateUniqueId()
    {
        // Arrange
        var service = new ShoppingListService();

        // Act
        var item1 = service.Add("Milk", 2, "Lactose-free");
        var item2 = service.Add("Bread", 1, "Whole grain");

        // Assert
        Assert.NotNull(item1!.Id);
        Assert.NotNull(item2!.Id);
        Assert.NotEqual(item1.Id, item2.Id);
    }

    //[Fact]
    //public void Add_ShouldIncrementItemCount()
    //{
    //    //Arrange
    //    var service = new ShoppingListService();
    //    var expected = service.GetAll();


    //    //Act
    //    var item1 = service.Add("Ost", 2, "Yummy");
    //    var item2 = service.Add("Ost", 1, "Yummy");


    //    //Assert
    //    Assert.Equal();
    //}



    ///GetAll() tests:
    /// - GetAll_WhenEmpty_ShouldReturnEmptyList
    /// - GetAll_WithItems_ShouldReturnAllItems
    /// - GetAll_ShouldNotReturnMoreThanActualItemCount
    [Fact]
    public void GetAll_WhenEmpty_ShouldReturnEmptyList()
    {
        // Arrange
        var service = new ShoppingListService();


        // Act
        var items = service.GetAll();

        // Assert
        Assert.NotEmpty(items);
    }

    [Fact]
    public void GetAll_WithItems_ShouldReturnAllItems()
    {
        // Arrange
        var service = new ShoppingListService();
        var expected = 4;
        // Act
        var items = service.GetAll();

        // Assert
        Assert.Equal(expected, items.Count);
    }

    //[Fact]
    //public void GetAll_ShouldNotReturnMoreThanActualItemCount()
    //{
    //    // Arrange
    //    var service = new ShoppingListService();
    //    var expected = 5;
    //    // Act
    //    var items = service.GetAll();
    //    // Assert
    //    Assert.NotEqual(expected, items.Count);
    //}

    /// GetById() tests:
    /// - GetById_WithValidId_ShouldReturnItem
    /// - GetById_WithInvalidId_ShouldReturnNull
    /// - GetById_WithNullId_ShouldReturnNull
    /// - GetById_WithEmptyId_ShouldReturnNull

    [Fact]
    public void GetById_WithValidId_ShouldReturnItem()
    {
        // Arrange
        var service = new ShoppingListService();
        var Item = service.Add("ost", 1, "test notes");
        var validId = Item!.Id;
        // Act
        var result = service.GetById(validId);
        // Assert
        Assert.Equal(Item, result);

    }

    [Fact]
    public void GetById_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var service = new ShoppingListService();
        var invalidId = "hssv";
        //Act
        var actual = service.GetById(invalidId);
        //Assert
        Assert.Null(actual);
    }

    /// Update() tests:
    /// - Update_WithValidId_ShouldUpdateAndReturnItem
    /// - Update_WithInvalidId_ShouldReturnNull
    /// - Update_ShouldNotChangeId
    /// - Update_ShouldNotChangeIsPurchased

    [Fact]
    public void Update_WithValidId_ShouldUpdateAndReturnItem()
    {
        // Arrange
        var service = new ShoppingListService();
        var item = service.Add("Bread", 1, "Whole grain");
        var validId = item!.Id;
        // Act
        var updatedItem = service.Update(validId, "Bread", 2, "Gluten-free");
        // Assert
        Assert.Equal("Bread", updatedItem!.Name);
        Assert.Equal(2, updatedItem.Quantity);
        Assert.Equal("Gluten-free", updatedItem.Notes);
    }

    [Fact]
    public void Update_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var service = new ShoppingListService();
        var invalidId = "något invalid";
        // Act
        var result = service.Update(invalidId, "Bread", 2, "Gluten-free");
        // Assert
        Assert.Null(result);
    }



    /// Delete() tests:
    /// - Delete_WithValidId_ShouldReturnTrue
    /// - Delete_WithInvalidId_ShouldReturnFalse
    /// - Delete_ShouldRemoveItemFromList
    /// - Delete_ShouldShiftRemainingItems
    /// - Delete_ShouldDecrementItemCount
    /// - Delete_LastItem_ShouldWork
    /// - Delete_FirstItem_ShouldWork
    /// - Delete_MiddleItem_ShouldWork

    [Fact]
    public void Delete_WithValidId_ShouldReturnTrue()
    {
        //Arrange
        var service = new ShoppingListService();
        var validId = service.GetAll()[0].Id;

        //Act
        var result = service.Delete(validId);


        //Assert
        Assert.True(result);

    }

    [Fact]
    public void Delete_WithInvalidId_ShouldReturnFalse()
    {
        //Arrange
        var service = new ShoppingListService();
        var invalidId = "invalid-id";
        //act
        var result = service.Delete(invalidId);
        //assert
        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldShiftRemainingItems()
    {
        //arrange
        var service = new ShoppingListService();
        var addedItem1 = service.Add("Eggs", 12, "Free-range");
        var addedItem2 = service.Add("Butter", 1, "Salted");
        var item1Id = addedItem1!.Id;
        var Expected = addedItem2.Id[0];

        //act
        var actual = service.Delete(item1Id);

        //assert
        Assert.Equal(Expected, addedItem2.Id[0]);
    }
}

