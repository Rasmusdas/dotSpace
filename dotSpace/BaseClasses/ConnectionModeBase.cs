﻿using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;

namespace dotSpace.BaseClasses
{
    /// <summary>
    /// Provides basic functionality for validating messages. This is an abstract class.
    /// </summary>
    public abstract class ConnectionModeBase : IConnectionMode
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected IProtocol protocol;
        protected IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the ConnectionModeBase class.
        /// </summary>
        public ConnectionModeBase(IProtocol protocol, IEncoder encoder)
        {
            this.protocol = protocol;
            this.encoder = encoder;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Template method for processing requests by the SpaceRepository executing the requested action.
        /// </summary>
        public abstract void ProcessRequest(OperationMap operationMap);
        /// <summary>
        /// Template method for executing a request by the RemoteSpace.
        /// </summary>
        public abstract T PerformRequest<T>(BasicRequest request) where T : BasicResponse;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Validates the passed response. If the message is a response and the response code is OK the message is returned; otherwise an exception is thrown.
        /// </summary>
        protected BasicResponse ValidateResponse(MessageBase message)
        {
            if (message is BasicResponse)
            {
                BasicResponse response = (BasicResponse)message;
                if (response.Code == StatusCode.OK)
                {
                    return (BasicResponse)message;
                }
                throw new Exception(string.Format("{0} - {1}", response.Code, response.Message));
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_RESPONSE, StatusMessage.BAD_RESPONSE));
        }
        /// <summary>
        /// Validates the passed Request. If the message is a request the message is returned; otherwise an exception is thrown.
        /// </summary>
        protected BasicRequest ValidateRequest(MessageBase message)
        {
            if (message is BasicRequest)
            {
                return (BasicRequest)message;
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_REQUEST, StatusMessage.BAD_REQUEST));
        } 

        #endregion
    }
}
