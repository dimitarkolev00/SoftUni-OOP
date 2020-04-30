using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Chainblock.Common;
using Chainblock.Contracts;
using Chainblock.Models;
using NUnit.Framework;
using Chainblock = Chainblock.Core.Chainblock;

namespace Chainblock.Tests
{
    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;
        private ITransaction testTransaction;

        [SetUp]
        public void SetUp()
        {
            this.chainblock = new Core.Chainblock();
            this.testTransaction = new Transaction(1, TransactionStatus.Unauthorized,
                "Pesho", "Gosho", 10);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            IChainblock chainblock = new Core.Chainblock();

            Assert.AreEqual(0, chainblock.Count);
        }

        [Test]
        public void AddShouldIncreaseCountWhenSuccess()
        {
            ITransaction transaction = new Transaction
                (1, TransactionStatus.Successfull, "Pesho", "Gosho", 10);

            this.chainblock.Add(transaction);

            Assert.AreEqual(1, this.chainblock.Count);
        }

        [Test]
        public void AddingSameTransactionWithSameIdShouldPass()
        {
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(1, ts, from, to, amount);
            ITransaction transactionCopy = new Transaction(2, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(transactionCopy);

            Assert.AreEqual(2, this.chainblock.Count);
        }

        [Test]
        public void AddingExistingTransactionShouldThrowException()
        {
            ITransaction transaction = new Transaction
                (1, TransactionStatus.Successfull, "Pesho", "Gosho", 10);

            this.chainblock.Add(transaction);

            Assert.That(() => { this.chainblock.Add(transaction); },
                Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.AddingExistingTransaction));
        }

        [Test]
        public void ContainsByTransactionShouldReturnTrueWithExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByTransactionShouldReturnFalseWithNonExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            Assert.IsFalse(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainsByIdShouldReturnTrueWithExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(id));
        }

        [Test]
        public void ContainsByIdShouldReturnFalseWithNonExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        public void TestChangingTransactionStatusOfExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            TransactionStatus newStatus = TransactionStatus.Successfull;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            this.chainblock.ChangeTransactionStatus(id, newStatus);

            Assert.AreEqual(newStatus, transaction.Status);
        }

        [Test]
        public void ChangingStatusOfNonExistingTransactionShouldThrowException()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            TransactionStatus newStatus = TransactionStatus.Successfull;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.That(() => { this.chainblock.ChangeTransactionStatus(13, newStatus); },
                Throws.ArgumentException.With.Message.EqualTo
                    (ExceptionMessages.ChangingStatusOfNonExistingTr));

        }

        [Test]
        public void GetByIdShouldReturnCorrectTransaction()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 20;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(testTransaction);

            ITransaction returnedTransaction = this.chainblock.GetById(id);

            Assert.AreEqual(transaction, returnedTransaction);
        }

        [Test]
        public void GetByIdShouldThrowExceptionWhenTryingToFindNonExistingTransaction()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 20;

            int fakeID = 13;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(testTransaction);

            Assert.That(() => { this.chainblock.GetById(fakeID); },
                Throws.Exception.With.Message.EqualTo(ExceptionMessages.NonExistingTransactionMessage));
        }

        [Test]
        public void RemovingTransactionShouldDecreaseCount()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(testTransaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.AreEqual(1, this.chainblock.Count);
        }

        [Test]
        public void RemovingTransactionShouldPhysicallyRemoveTheTx()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(testTransaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.That(() => { this.chainblock.GetById(id); },
                Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.NonExistingTransactionMessage));
        }

        [Test]
        public void RemovingNonExistingTransactionShouldThrowException()
        {
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(testTransaction);

            Assert.That(() => { this.chainblock.RemoveTransactionById(13); },
                Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages
                    .RemovingNonExistingTransactionMessage));
        }

        [Test]
        public void GetTransactionsStatusShouldReturnOrderedCollectionWithRightTransaction()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransactions.Add(currTr);
                }

                this.chainblock.Add(currTr);
            }

            ITransaction succTr = new Transaction(5, TransactionStatus.Successfull,
                "Pesho4", "Gosho4", 15);

            expTransactions.Add(succTr);
            this.chainblock.Add(succTr);

            IEnumerable actTransactions = this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull);

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount).ToList();

            CollectionAssert.AreEqual(expTransactions, actTransactions);
        }

        [Test]
        public void GettingTransactionWithNonExistingStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            Assert.That(() => { this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull); },
                Throws.Exception.With.Message.EqualTo(ExceptionMessages.EmptyStatusTransactionCollection));
        }

        [Test]
        public void AllSendersWithStatusShouldReturnCorrectOrderedCollection()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransactions.Add(currTr);
                }

                this.chainblock.Add(currTr);
            }

            ITransaction succTr = new Transaction(5, TransactionStatus.Successfull,
                "Pesho4", "Gosho4", 15);

            expTransactions.Add(succTr);
            this.chainblock.Add(succTr);

            IEnumerable<string> actTransactionsOut = this.chainblock.GetAllSendersWithTransactionStatus
                (TransactionStatus.Successfull);

            IEnumerable<string> expTransactionsOut = expTransactions
                .OrderByDescending(tr => tr.Amount)
                .Select(tr => tr.From);

            CollectionAssert.AreEqual(expTransactionsOut, actTransactionsOut);
        }

        [Test]
        public void AllSendersByStatusShouldThrowExceptionWhenThereAneNoTrWithGivenStats()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            Assert.That(() => { this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Successfull); },
                Throws.InvalidOperationException.With.Message
                    .EqualTo(ExceptionMessages.EmptyStatusTransactionCollection));
        }

        [Test]
        public void AllReceiversWithStatusShouldReturnCorrectOrderedCollection()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransactions.Add(currTr);
                }

                this.chainblock.Add(currTr);
            }

            ITransaction succTr = new Transaction(5, TransactionStatus.Successfull,
                "Pesho4", "Gosho4", 15);

            expTransactions.Add(succTr);
            this.chainblock.Add(succTr);

            IEnumerable<string> actTransactionsOut = this.chainblock.GetAllReceiversWithTransactionStatus
                (TransactionStatus.Successfull);

            IEnumerable<string> expTransactionsOut = expTransactions
                .OrderByDescending(tr => tr.Amount)
                .Select(tr => tr.To);

            CollectionAssert.AreEqual(expTransactionsOut, actTransactionsOut);
        }

        [Test]
        public void AllReceiversByStatusShouldThrowExceptionWhenThereAneNoTrWithGivenStats()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 * (i + 1);

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            Assert.That(() => { this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Successfull); },
                Throws.InvalidOperationException.With.Message
                    .EqualTo(ExceptionMessages.EmptyStatusTransactionCollection));
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithNoDuplicatedAmounts()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)(i % 4);
                string from = "Pesho" + 1;
                string to = "Gosho" + 1;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
                expTransactions.Add(currTr);

            }

            IEnumerable<ITransaction> actTr = this.chainblock.GetAllOrderedByAmountDescendingThenById();
            expTransactions = expTransactions.OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id)
                .ToList();

            CollectionAssert.AreEqual(expTransactions, actTr);
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithDuplicatedAmounts()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)(i % 4);
                string from = "Pesho" + 1;
                string to = "Gosho" + 1;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
                expTransactions.Add(currTr);

            }

            ITransaction transaction = new Transaction(11, TransactionStatus.Successfull,
                "Gosho11", "Pesho11", 10);
            this.chainblock.Add(transaction);
            expTransactions.Add(transaction);

            IEnumerable<ITransaction> actTr = this.chainblock.GetAllOrderedByAmountDescendingThenById();
            expTransactions = expTransactions.OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id)
                .ToList();

            CollectionAssert.AreEqual(expTransactions, actTr);
        }

        [Test]
        public void TestGetAllOrderedByAmountThenByIdWithEmptyCollection()
        {
            IEnumerable<ITransaction> actTr = this.chainblock.GetAllOrderedByAmountDescendingThenById();

            CollectionAssert.IsEmpty(actTr);
        }

        [Test]
        public void TesIfGetAllBySenderDescendingByAmountWorksCorrectly()
        {
            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            string wantedSender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = wantedSender;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
                expTransactions.Add(currTr);
            }
            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            IEnumerable<ITransaction> actTr = this.chainblock
                .GetBySenderOrderedByAmountDescending(wantedSender);

            expTransactions = expTransactions
                .OrderByDescending(tr => tr.Amount)
                .ToList();

            CollectionAssert.AreEqual(expTransactions, actTr);
        }

        [Test]
        public void TestGetAllByNonExistingSenderDescendingByAmount()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            string wantedSender = "Pesho";

            Assert.That(() =>
            {
                this.chainblock.GetBySenderOrderedByAmountDescending(wantedSender);
            }, Throws.InvalidOperationException.With.Message.EqualTo
                (ExceptionMessages.NoTransactionsForGivenSenderMessage));
        }

        [Test]
        public void TestIfGetByReceiverDescendingByAmountThenByIdWorksCorrectlyWithNoDuplicatedAmounts()
        {

            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            string wantedReceiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = wantedReceiver;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
                expTransactions.Add(currTr);
            }
            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            IEnumerable<ITransaction> actTr = this.chainblock
                .GetByReceiverOrderedByAmountThenById(wantedReceiver);

            expTransactions = expTransactions
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id)
                .ToList();

            CollectionAssert.AreEqual(expTransactions, actTr);
        }
        [Test]
        public void TestIfGetByReceiverDescendingByAmountThenByIdWorksCorrectlyWithDuplicatedAmounts()
        {

            ICollection<ITransaction> expTransactions = new List<ITransaction>();

            string wantedReceiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = wantedReceiver;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
                expTransactions.Add(currTr);
            }
            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            ITransaction tr = new Transaction(11, TransactionStatus.Successfull
                , "Pesho11", "Gosho11", 10);

            IEnumerable<ITransaction> actTr = this.chainblock
                .GetByReceiverOrderedByAmountThenById(wantedReceiver);

            expTransactions = expTransactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id)
                .ToList();

            CollectionAssert.AreEqual(expTransactions, actTr);
        }

        [Test]
        public void GetByReceiverDescendingByAmountThenByIdShouldThrowExceptionWhenNoTransactionsFound()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currTr = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currTr);
            }

            string wantedReceiver = "Gosho";

            Assert.That(() =>
            {
                this.chainblock.GetByReceiverOrderedByAmountThenById(wantedReceiver);
            }, Throws.InvalidOperationException.With.Message.EqualTo
                (ExceptionMessages.NoTransactionsForGivenReceiverMessage));
        }
    }
}
