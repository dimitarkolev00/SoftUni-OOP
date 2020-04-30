using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Chainblock.Common;
using Chainblock.Contracts;

namespace Chainblock.Core
{
    public class Chainblock : IChainblock
    {
        private ICollection<ITransaction> transactions;
        public Chainblock()
        {
            this.transactions = new List<ITransaction>();
        }

        public int Count => this.transactions.Count;

        public void Add(ITransaction tx)
        {
            if (this.transactions.Contains(tx))
            {
                throw new InvalidOperationException(ExceptionMessages.AddingExistingTransaction);
            }
            this.transactions.Add(tx);
        }

        public bool Contains(ITransaction tx)
        {
            return this.Contains(tx.Id);
        }

        public bool Contains(int id)
        {
            bool isContained = this.transactions.Any(tr => tr.Id == id);
            return isContained;
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            ITransaction transaction = this.transactions.FirstOrDefault(tr => tr.Id == id);

            if (transaction == null)
            {
                throw new ArgumentException(ExceptionMessages.ChangingStatusOfNonExistingTr);
            }

            transaction.Status = newStatus;
        }

        public void RemoveTransactionById(int id)
        {
            try
            {
                ITransaction transaction = this.GetById(id);

                this.transactions.Remove(transaction);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(ExceptionMessages.RemovingNonExistingTransactionMessage);
            }


        }

        public ITransaction GetById(int id)
        {
            ITransaction transaction = this.transactions.FirstOrDefault(tx => tx.Id == id);

            if (transaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NonExistingTransactionMessage);
            }

            return transaction;
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            IEnumerable<ITransaction> transactions = this.transactions
                .Where(tx => tx.Status == status)
                .OrderByDescending(tr => tr.Amount);

            if (transactions.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyStatusTransactionCollection);
            }

            return transactions;
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> senders = this.GetByTransactionStatus(status)
                .Select(tr => tr.From);

            return senders;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> receivers = this.GetByTransactionStatus(status)
                .Select(tr => tr.To);

            return receivers;
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            IEnumerable<ITransaction> transactions = this.transactions
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            return transactions;
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            IEnumerable<ITransaction> transactions = this.transactions
                .Where(tr => tr.From == sender)
                .OrderByDescending(tr => tr.Amount);
            if (transactions.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoTransactionsForGivenSenderMessage);
            }
            return transactions;
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            IEnumerable<ITransaction> transactions = this.transactions
                .Where(tr => tr.To == receiver)
                .OrderByDescending(tr => tr.Amount)
                .ThenBy(tr => tr.Id);

            if (transactions.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoTransactionsForGivenReceiverMessage);
            }

            return transactions;
        }
        public IEnumerator<ITransaction> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.transactions.ToArray()[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
            ;
        }
    }
}
