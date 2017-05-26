using HCI_Project.Protocol;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request
{
    public abstract class RequestHandler<TSubject, TOperationCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected RequestHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(TOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            ReturnCode errorCode;
            if (CheckParameters(parameters, out errorCode, out errorMessage))
            {
                return true;
            }
            else
            {
                SendResponse(operationCode, errorCode, errorMessage, new Dictionary<byte, object>());
                return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out ReturnCode errorCode, out string errorMessage)
        {
            if (parameters.Count == correctParameterCount)
            {
                errorCode = ReturnCode.Successful;
                errorMessage = "";
                return true;
            }
            else
            {
                errorCode = ReturnCode.ParameterCountError;
                errorMessage = $"Parameter Count: {parameters.Count} Should be {correctParameterCount}";
                return false;
            }
        }
        internal abstract void SendResponse(TOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters);
    }
}
