using HCI_Project.Protocol;
using HCI_Project.Protocol.Communication.FetchDataRequestParameters;
using System.Collections.Generic;

namespace HCI_Project.Library.CommunicationInfrastructure.Request.Handlers
{
    public abstract class FetchDataRequestBroker<TSubject, TOperationCode, TFetchDataCode> : RequestHandler<TSubject, TOperationCode>
    {
        internal readonly Dictionary<TFetchDataCode, FetchDataRequestHandler<TSubject, TFetchDataCode>> fetchTable;

        public FetchDataRequestBroker(TSubject subject) : base(subject, 2)
        {
            fetchTable = new Dictionary<TFetchDataCode, FetchDataRequestHandler<TSubject, TFetchDataCode>>();
        }

        internal override bool Handle(TOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                TFetchDataCode fetchCode = (TFetchDataCode)parameters[(byte)FetchDataRequestParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataRequestParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"{subject.GetType()} Fetch Operation Not Exist Fetch Code: {fetchCode}";
                    SendResponse(operationCode, ReturnCode.UndefinedOperation, errorMessage, new Dictionary<byte, object>());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
