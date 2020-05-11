using System.Collections.Generic;

namespace QuestOrAssess.UserIdentity.Core.Domain
{
    public class OutResult
    {
        public OutResult()
        {
            IsValid = true;
            Message = "Success";
        }

        public OutResult(string message, bool status)
        {
            IsValid = status;
            Message = message;
        }

        public OutResult(string message, string exception, bool status)
        {
            IsValid = status;
            Message = message;
            FullException = exception;
        }

        public static OutResult MultipleMessages(bool status, IEnumerable<string> errors)
        {
            return new OutResult(string.Join(". ", errors), status);
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public string FullException { get; set; }


        #region Success Messages

        public static OutResult Success_Created()
        {
            return new OutResult("Created successfully.", true);
        }

        public static OutResult Success_Updated()
        {
            return new OutResult("Updated successfully.", true);
        }

        public static OutResult Success_Deleted()
        {
            return new OutResult("Deleted successfully.", true);
        }

        #endregion


        #region Failure Messages

        public static OutResult Error_TryingToInsertNull()
        {
            return new OutResult("Trying to insert Null", false);
        }

        public static OutResult Error_TryingToUpdateNull()
        {
            return new OutResult("Trying to update Null", false);
        }

        public static OutResult Error_TryingToDeleteNull()
        {
            return new OutResult("Trying to delete Null", false);
        }

        public static OutResult Error_Generic()
        {
            return new OutResult("We apologies. Something went wrong on server.", false);
        }

        public static OutResult Error_AlreadyExists()
        {
            return new OutResult("An item with the same name already exists.", false);
        }

        public static OutResult Error_SelfDestructionNotAllowed()
        {
            return new OutResult("Deleting your own account is not allowed. Please contact system administrator if you still want your account to be deleted.", false);
        }

        #endregion



























    }
}
