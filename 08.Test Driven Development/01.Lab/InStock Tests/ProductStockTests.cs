using System;
using System.Linq;

namespace INStock.Tests
{
    using NUnit.Framework;
    [TestFixture]
    public class ProductStockTests
    {
        private const string ProductLabel = "Test";
        private const string AnotherProductLabel = "Another Test";

        private ProductStock productStock;
        private Product product;
        private Product anotherProduct;

        [SetUp]
        public void SetUp()
        {
            this.productStock = new ProductStock();
            this.product = new Product(ProductLabel, 10, 1);
            this.anotherProduct = new Product(AnotherProductLabel, 10, 20);
        }

        [Test]
        public void AddProductShouldSaveTheProduct()
        {
            this.productStock.Add(this.product);

            var productInStock = this.productStock.FindByLabel(ProductLabel);

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(ProductLabel));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void AddProductShouldThrowsExceptionWithDuplicatedLabel()
        {
            Assert.That(() =>
            {
                this.productStock.Add(product);
                this.productStock.Add(product);
            }, Throws.Exception.InstanceOf<ArgumentException>()
                .With.Message.EqualTo($"A product with '{ProductLabel}' already exists."));
        }
        [Test]
        public void AddingTwoProductsShouldSaveThem()
        {


            this.productStock.Add(this.product);
            this.productStock.Add(this.anotherProduct);

            var firstProductInStock = this.productStock.FindByLabel(ProductLabel);
            var secondProductInStock = this.productStock.FindByLabel(AnotherProductLabel);

            Assert.That(firstProductInStock, Is.Not.Null);
            Assert.That(firstProductInStock.Label, Is.EqualTo(ProductLabel));
            Assert.That(firstProductInStock.Price, Is.EqualTo(10));
            Assert.That(firstProductInStock.Quantity, Is.EqualTo(1));

            Assert.That(secondProductInStock, Is.Not.Null);
            Assert.That(secondProductInStock.Label, Is.EqualTo(AnotherProductLabel));
            Assert.That(secondProductInStock.Price, Is.EqualTo(10));
            Assert.That(secondProductInStock.Quantity, Is.EqualTo(20));
        }
        [Test]
        public void RemoveShouldThrowsExceptionWhenProductIsNull()
        {

            Assert.That(
                () => this.productStock.Remove(null),
                Throws.Exception.InstanceOf<ArgumentException>().With.Message
                    .EqualTo("Product cannot be null.")
            );
        }
        [Test]
        public void RemoveShouldReturnTrueWhenProductIsRemoved()
        {

            this.AddMultipleProductsToProductStock();
            var productToRemove = this.productStock.Find(3);

            var result = this.productStock.Remove(productToRemove);

            Assert.That(result, Is.True);
            Assert.That(this.productStock.Count, Is.EqualTo(4));
            Assert.That(this.productStock[3].Label, Is.EqualTo("5"));

        }
        [Test]
        public void RemoveShouldReturnFalseWhenProductIsNotFound()
        {

            this.AddMultipleProductsToProductStock();
            var productNotInStock = new Product(ProductLabel, 10, 20);

            var result = this.productStock.Remove(productNotInStock);

            Assert.That(result, Is.False);
            Assert.That(this.productStock.Count, Is.EqualTo(5));

        }

        [Test]
        public void ContainsShouldThrowsExceptionWhenProductIsNull()
        {

            Assert.That(
                    () => this.productStock.Add(null),
                    Throws.Exception.InstanceOf<ArgumentException>().With.Message
                        .EqualTo("Product cannot be null.")
            );
        }

        [Test]
        public void ContainsShouldReturnTrueWhenProductExists()
        {
            this.productStock.Add(product);

            var result = this.productStock.Contains(product);

            Assert.That(result, Is.True);
        }
        [Test]
        public void ContainsShouldReturnFalseWhenProductDoesNotExists()
        {
            var result = this.productStock.Contains(product);

            Assert.That(result, Is.False);
        }

        [Test]
        public void CountShouldReturnCorrectProductCount()
        {
            this.productStock.Add(product);
            this.productStock.Add(this.anotherProduct);

            var result = this.productStock.Count;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void FindShouldReturnTheCorrectProductByGivenIndex()
        {
            this.productStock.Add(product);
            this.productStock.Add(this.anotherProduct);

            var productInStock = this.productStock.Find(1);

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(AnotherProductLabel));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(20));
        }

        [Test]
        public void FindShouldThrowsExceptionWhenIndexIsOutOfRange()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock.Find(1),
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }
        [Test]
        public void FindShouldThrowsExceptionWhenIndexIsBelowZer()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock.Find(-1),
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }
        [Test]
        public void FindByLabelShouldThrowsExceptionWhenLabelIsNull()
        {

            Assert.That(
                () => this.productStock.FindByLabel(null),
                Throws.Exception.InstanceOf<ArgumentException>().With.Message
                    .EqualTo("Product label cannot be null.")
            );
        }
        [Test]
        public void FindByLabelShouldThrowsExceptionWhenLabelDoesNotExist()
        {
            const string invalidLabel = "Invalid Label";

            Assert.That(
                () => this.productStock.FindByLabel(invalidLabel),
                Throws.Exception.InstanceOf<ArgumentException>().With.Message
                    .EqualTo($"Product with '{invalidLabel}' label could not be found.")
            );
        }

        [Test]
        public void FindByLabelShouldReturnCorrectProduct()
        {
            this.productStock.Add(product);
            this.productStock.Add(this.anotherProduct);

            var productInStock = this.productStock.FindByLabel(ProductLabel);

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(ProductLabel));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void FindAllInPriceRangeShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllInRange(30, 50);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindAllInPriceRangeShouldReturnCorrectCollectionInCorrectOrder()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllInRange(4, 21).ToList();

            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result[0].Price, Is.EqualTo(20));
            Assert.That(result[1].Price, Is.EqualTo(10));
            Assert.That(result[2].Price, Is.EqualTo(5));
        }

        [Test]
        public void FindAllByPriceShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllByPrice(30);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void FindAllByPriceShouldReturnCorrectCollection()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllByPrice(400).ToList();

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].Label, Is.EqualTo("4"));
            Assert.That(result[1].Label, Is.EqualTo("5"));
        }
        [Test]
        public void FindMostExpensiveProductShouldThrowExceptionWhenProductStockIsEmpty()
        {
            Assert.That(
                () => this.productStock.FindMostExpensiveProduct(),
                Throws.Exception.InstanceOf<InvalidOperationException>().With.Message
                    .EqualTo("Product stock is empty.")
            );
        }

        [Test]
        public void FindMostExpensiveProductShouldReturnCorrectProduct()
        {
            this.AddMultipleProductsToProductStock();

            var productInStock = this.productStock.FindMostExpensiveProduct();

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo("4"));
            Assert.That(productInStock.Price, Is.EqualTo(400));
            Assert.That(productInStock.Quantity, Is.EqualTo(4));
        }

        [Test]
        public void FindAllByQuantityShouldReturnEmptyCollectionWhenNoProductMatch()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllByQuantity(6);

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void FindAllByQuantityShouldReturnCorrectCollection()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.FindAllByQuantity(5).ToList();

            Assert.That(result.Count, Is.EqualTo(1));

            Assert.That(result[0].Label, Is.EqualTo("5"));

        }
        [Test]
        public void GetEnumeratorShouldReturnCorrectInsertionOrder()
        {
            this.AddMultipleProductsToProductStock();

            var result = this.productStock.ToList();

            Assert.That(result[0].Label, Is.EqualTo("1"));
            Assert.That(result[1].Label, Is.EqualTo("2"));
            Assert.That(result[2].Label, Is.EqualTo("3"));
            Assert.That(result[3].Label, Is.EqualTo("4"));
            Assert.That(result[4].Label, Is.EqualTo("5"));
        }

        [Test]
        public void GetIndexShouldReturnCorrectProductByIndex()
        {
            this.productStock.Add(product);
            this.productStock.Add(this.anotherProduct);

            var productInStock = this.productStock[1];

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo(AnotherProductLabel));
            Assert.That(productInStock.Price, Is.EqualTo(10));
            Assert.That(productInStock.Quantity, Is.EqualTo(20));
        }
        [Test]
        public void GetIndexShouldThrowsExceptionWhenIndexIsOutOfRange()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock[1],
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }
        [Test]
        public void GetIndexShouldThrowsExceptionWhenIndexIsBelowZero()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock[-1],
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }

        [Test]
        public void SetIndexShouldChangeProduct()
        {

            const string productLabel = "Yet Another Test";

            this.AddMultipleProductsToProductStock();

            this.product = new Product(productLabel, 50, 3);

            var productInStock = this.productStock.Find(3);

            Assert.That(productInStock, Is.Not.Null);
            Assert.That(productInStock.Label, Is.EqualTo("4"));
            Assert.That(productInStock.Price, Is.EqualTo(400));
            Assert.That(productInStock.Quantity, Is.EqualTo(4));

        }
        [Test]
        public void SetIndexShouldThrowsExceptionWhenIndexIsOutOfRange()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock[1] = new Product(ProductLabel, 10, 10),
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }
        [Test]

        public void SetIndexShouldThrowsExceptionWhenIndexIsBelowZero()
        {
            this.productStock.Add(product);

            Assert.That(
                () => this.productStock[-1] = new Product(ProductLabel, 10, 10),
                Throws.InstanceOf<IndexOutOfRangeException>().With.Message
                    .EqualTo("Product index does not exist.")
            );
        }


        private void AddMultipleProductsToProductStock()
        {
            this.productStock.Add(new Product("1", 10, 1));
            this.productStock.Add(new Product("2", 5, 2));
            this.productStock.Add(new Product("3", 20, 3));
            this.productStock.Add(new Product("4", 400, 4));
            this.productStock.Add(new Product("5", 400, 5));
        }
    }
}
