/*
 * Copyright 2010-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

/*
 * Do not modify this file. This file is generated from the cognito-identity-2014-06-30.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CognitoIdentity.Model
{
    /// <summary>
    /// Container for the parameters to the CreateIdentityPool operation.
    /// Creates a new identity pool. The identity pool is a store of user identity information
    /// that is specific to your AWS account. The limit on identity pools is 60 per account.
    /// The keys for <code>SupportedLoginProviders</code> are as follows: <ul> <li>Facebook:
    /// <code>graph.facebook.com</code></li> <li>Google: <code>accounts.google.com</code></li>
    /// <li>Amazon: <code>www.amazon.com</code></li> <li>Twitter: <code>api.twitter.com</code></li>
    /// <li>Digits: <code>www.digits.com</code></li> </ul> You must use AWS Developer credentials
    /// to call this API.
    /// </summary>
    public partial class CreateIdentityPoolRequest : AmazonCognitoIdentityRequest
    {
        private bool? _allowUnauthenticatedIdentities;
        private List<CognitoIdentityProviderInfo> _cognitoIdentityProviders = new List<CognitoIdentityProviderInfo>();
        private string _developerProviderName;
        private string _identityPoolName;
        private List<string> _openIdConnectProviderARNs = new List<string>();
        private List<string> _samlProviderARNs = new List<string>();
        private Dictionary<string, string> _supportedLoginProviders = new Dictionary<string, string>();

        /// <summary>
        /// Gets and sets the property AllowUnauthenticatedIdentities. 
        /// <para>
        /// TRUE if the identity pool supports unauthenticated logins.
        /// </para>
        /// </summary>
        public bool AllowUnauthenticatedIdentities
        {
            get { return this._allowUnauthenticatedIdentities.GetValueOrDefault(); }
            set { this._allowUnauthenticatedIdentities = value; }
        }

        // Check to see if AllowUnauthenticatedIdentities property is set
        internal bool IsSetAllowUnauthenticatedIdentities()
        {
            return this._allowUnauthenticatedIdentities.HasValue; 
        }

        /// <summary>
        /// Gets and sets the property CognitoIdentityProviders. 
        /// <para>
        /// An array of Amazon Cognito Identity user pools.
        /// </para>
        /// </summary>
        public List<CognitoIdentityProviderInfo> CognitoIdentityProviders
        {
            get { return this._cognitoIdentityProviders; }
            set { this._cognitoIdentityProviders = value; }
        }

        // Check to see if CognitoIdentityProviders property is set
        internal bool IsSetCognitoIdentityProviders()
        {
            return this._cognitoIdentityProviders != null && this._cognitoIdentityProviders.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property DeveloperProviderName. 
        /// <para>
        /// The "domain" by which Cognito will refer to your users. This name acts as a placeholder
        /// that allows your backend and the Cognito service to communicate about the developer
        /// provider. For the <code>DeveloperProviderName</code>, you can use letters as well
        /// as period (<code>.</code>), underscore (<code>_</code>), and dash (<code>-</code>).
        /// </para>
        ///  
        /// <para>
        /// Once you have set a developer provider name, you cannot change it. Please take care
        /// in setting this parameter.
        /// </para>
        /// </summary>
        public string DeveloperProviderName
        {
            get { return this._developerProviderName; }
            set { this._developerProviderName = value; }
        }

        // Check to see if DeveloperProviderName property is set
        internal bool IsSetDeveloperProviderName()
        {
            return this._developerProviderName != null;
        }

        /// <summary>
        /// Gets and sets the property IdentityPoolName. 
        /// <para>
        /// A string that you provide.
        /// </para>
        /// </summary>
        public string IdentityPoolName
        {
            get { return this._identityPoolName; }
            set { this._identityPoolName = value; }
        }

        // Check to see if IdentityPoolName property is set
        internal bool IsSetIdentityPoolName()
        {
            return this._identityPoolName != null;
        }

        /// <summary>
        /// Gets and sets the property OpenIdConnectProviderARNs. 
        /// <para>
        /// A list of OpendID Connect provider ARNs.
        /// </para>
        /// </summary>
        public List<string> OpenIdConnectProviderARNs
        {
            get { return this._openIdConnectProviderARNs; }
            set { this._openIdConnectProviderARNs = value; }
        }

        // Check to see if OpenIdConnectProviderARNs property is set
        internal bool IsSetOpenIdConnectProviderARNs()
        {
            return this._openIdConnectProviderARNs != null && this._openIdConnectProviderARNs.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property SamlProviderARNs. 
        /// <para>
        /// An array of Amazon Resource Names (ARNs) of the SAML provider for your identity pool.
        /// </para>
        /// </summary>
        public List<string> SamlProviderARNs
        {
            get { return this._samlProviderARNs; }
            set { this._samlProviderARNs = value; }
        }

        // Check to see if SamlProviderARNs property is set
        internal bool IsSetSamlProviderARNs()
        {
            return this._samlProviderARNs != null && this._samlProviderARNs.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property SupportedLoginProviders. 
        /// <para>
        /// Optional key:value pairs mapping provider names to provider app IDs.
        /// </para>
        /// </summary>
        public Dictionary<string, string> SupportedLoginProviders
        {
            get { return this._supportedLoginProviders; }
            set { this._supportedLoginProviders = value; }
        }

        // Check to see if SupportedLoginProviders property is set
        internal bool IsSetSupportedLoginProviders()
        {
            return this._supportedLoginProviders != null && this._supportedLoginProviders.Count > 0; 
        }

    }
}