using System.Reflection.Metadata;

namespace Chainblock.Common
{
    public static class ExceptionMessages
    {
        public static string InvalidIdMessage = "IDs cannot be zero or negative!";
        public static string InvalidSenderUsernameMessage = "Sender name cannot be empty or whitespace !";
        public static string InvalidReceiverUsernameMessage = "Receiver name cannot be empty or whitespace !";
        public static string InvalidTransactionAmount = "Transaction amount should be greater than 0!";

        public static string AddingExistingTransaction = "Transaction already exists in our records!";
        public static string ChangingStatusOfNonExistingTr = "You can't change the status of non existing transaction!";
        public static string NonExistingTransactionMessage = "Transaction with given ID not found!";
        public static string RemovingNonExistingTransactionMessage = "You cannot remove non existing transaction!";
        public static string EmptyStatusTransactionCollection = "There are not transactions matching provided " +
                                                                "provided transaction status!";
        public static string NoTransactionsForGivenSenderMessage = "There are no corresponding transactions" +
                                                                   " to given sender name!";
        public static string NoTransactionsForGivenReceiverMessage = "There are no corresponding transactions" +
                                                                   " to given receiver name!";

    }
}
