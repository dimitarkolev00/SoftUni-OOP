﻿using System;
using Chainblock.Common;
using Chainblock.Contracts;
using Chainblock.Models;
using NUnit.Framework;

namespace Chainblock.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            Assert.AreEqual(id, transaction.Id);
            Assert.AreEqual(ts, transaction.Status);
            Assert.AreEqual(from, transaction.From);
            Assert.AreEqual(to, transaction.To);
            Assert.AreEqual(amount, transaction.Amount);
        }

        [Test]
        [TestCase(-10)]
        [TestCase(0)]
        public void TestWithLikeInvalidId(int id)
        {
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.
                EqualTo(ExceptionMessages.InvalidIdMessage));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void TestWithLikeInvalidSenderName(string from)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string to = "Gosho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.
                EqualTo(ExceptionMessages.InvalidSenderUsernameMessage));
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void TestWithLikeInvalidReceiverName(string to)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.
                EqualTo(ExceptionMessages.InvalidReceiverUsernameMessage));
        }

        [Test]
        [TestCase(0.0)]
        [TestCase(-10.0)]
        [TestCase(-0.00001)]
        public void TestWithLikeInvalidAmount(double amount)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.
                EqualTo(ExceptionMessages.InvalidTransactionAmount));
        }


    }
}
