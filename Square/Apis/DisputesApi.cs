using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Square;
using Square.Utilities;
using Square.Http.Request;
using Square.Http.Response;
using Square.Http.Client;
using Square.Authentication;
using Square.Exceptions;

namespace Square.Apis
{
    internal class DisputesApi: BaseApi, IDisputesApi
    {
        internal DisputesApi(IConfiguration config, IHttpClient httpClient, IDictionary<string, IAuthManager> authManagers, HttpCallBack httpCallBack = null) :
            base(config, httpClient, authManagers, httpCallBack)
        { }

        /// <summary>
        /// Returns a list of disputes associated
        /// with a particular account.
        /// </summary>
        /// <param name="cursor">Optional parameter: A pagination cursor returned by a previous call to this endpoint. Provide this to retrieve the next set of results for the original query. For more information, see [Paginating](https://developer.squareup.com/docs/basics/api101/pagination).</param>
        /// <param name="states">Optional parameter: The dispute states to filter the result. If not specified, the endpoint returns all open disputes (dispute status is not `INQUIRY_CLOSED`, `WON`, or `LOST`).</param>
        /// <param name="locationId">Optional parameter: The ID of the location for which to return  a list of disputes. If not specified, the endpoint returns all open disputes (dispute status is not `INQUIRY_CLOSED`, `WON`, or  `LOST`) associated with all locations.</param>
        /// <return>Returns the Models.ListDisputesResponse response from the API call</return>
        public Models.ListDisputesResponse ListDisputes(string cursor = null, string states = null, string locationId = null)
        {
            Task<Models.ListDisputesResponse> t = ListDisputesAsync(cursor, states, locationId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns a list of disputes associated
        /// with a particular account.
        /// </summary>
        /// <param name="cursor">Optional parameter: A pagination cursor returned by a previous call to this endpoint. Provide this to retrieve the next set of results for the original query. For more information, see [Paginating](https://developer.squareup.com/docs/basics/api101/pagination).</param>
        /// <param name="states">Optional parameter: The dispute states to filter the result. If not specified, the endpoint returns all open disputes (dispute status is not `INQUIRY_CLOSED`, `WON`, or `LOST`).</param>
        /// <param name="locationId">Optional parameter: The ID of the location for which to return  a list of disputes. If not specified, the endpoint returns all open disputes (dispute status is not `INQUIRY_CLOSED`, `WON`, or  `LOST`) associated with all locations.</param>
        /// <return>Returns the Models.ListDisputesResponse response from the API call</return>
        public async Task<Models.ListDisputesResponse> ListDisputesAsync(string cursor = null, string states = null, string locationId = null, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes");

            //process optional query parameters
            ApiHelper.AppendUrlWithQueryParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "cursor", cursor },
                { "states", states },
                { "location_id", locationId }
            }, ArrayDeserializationFormat, ParameterSeparator);

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Get(_queryUrl,_headers);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.ListDisputesResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Returns details of a specific dispute.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want more details about.</param>
        /// <return>Returns the Models.RetrieveDisputeResponse response from the API call</return>
        public Models.RetrieveDisputeResponse RetrieveDispute(string disputeId)
        {
            Task<Models.RetrieveDisputeResponse> t = RetrieveDisputeAsync(disputeId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns details of a specific dispute.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want more details about.</param>
        /// <return>Returns the Models.RetrieveDisputeResponse response from the API call</return>
        public async Task<Models.RetrieveDisputeResponse> RetrieveDisputeAsync(string disputeId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Get(_queryUrl,_headers);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.RetrieveDisputeResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Accepts loss on a dispute. Square returns
        /// the disputed amount to the cardholder and updates the
        /// dispute state to ACCEPTED.
        /// Square debits the disputed amount from the seller’s Square
        /// account. If the Square account balance does not have
        /// sufficient funds, Square debits the associated bank account.
        /// For an overview of the Disputes API, see [Overview](https://developer.squareup.com/docs/docs/disputes-api/overview).
        /// </summary>
        /// <param name="disputeId">Required parameter: ID of the dispute you want to accept.</param>
        /// <return>Returns the Models.AcceptDisputeResponse response from the API call</return>
        public Models.AcceptDisputeResponse AcceptDispute(string disputeId)
        {
            Task<Models.AcceptDisputeResponse> t = AcceptDisputeAsync(disputeId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Accepts loss on a dispute. Square returns
        /// the disputed amount to the cardholder and updates the
        /// dispute state to ACCEPTED.
        /// Square debits the disputed amount from the seller’s Square
        /// account. If the Square account balance does not have
        /// sufficient funds, Square debits the associated bank account.
        /// For an overview of the Disputes API, see [Overview](https://developer.squareup.com/docs/docs/disputes-api/overview).
        /// </summary>
        /// <param name="disputeId">Required parameter: ID of the dispute you want to accept.</param>
        /// <return>Returns the Models.AcceptDisputeResponse response from the API call</return>
        public async Task<Models.AcceptDisputeResponse> AcceptDisputeAsync(string disputeId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/accept");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Post(_queryUrl, _headers, null);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.AcceptDisputeResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Returns a list of evidence associated with a dispute.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute.</param>
        /// <return>Returns the Models.ListDisputeEvidenceResponse response from the API call</return>
        public Models.ListDisputeEvidenceResponse ListDisputeEvidence(string disputeId)
        {
            Task<Models.ListDisputeEvidenceResponse> t = ListDisputeEvidenceAsync(disputeId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns a list of evidence associated with a dispute.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute.</param>
        /// <return>Returns the Models.ListDisputeEvidenceResponse response from the API call</return>
        public async Task<Models.ListDisputeEvidenceResponse> ListDisputeEvidenceAsync(string disputeId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/evidence");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Get(_queryUrl,_headers);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.ListDisputeEvidenceResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Removes specified evidence from a dispute.
        /// Square does not send the bank any evidence that
        /// is removed. Also, you cannot remove evidence after
        /// submitting it to the bank using [SubmitEvidence](https://developer.squareup.com/docs/reference/square/disputes-api/submit-evidence).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to remove evidence from.</param>
        /// <param name="evidenceId">Required parameter: The ID of the evidence you want to remove.</param>
        /// <return>Returns the Models.RemoveDisputeEvidenceResponse response from the API call</return>
        public Models.RemoveDisputeEvidenceResponse RemoveDisputeEvidence(string disputeId, string evidenceId)
        {
            Task<Models.RemoveDisputeEvidenceResponse> t = RemoveDisputeEvidenceAsync(disputeId, evidenceId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Removes specified evidence from a dispute.
        /// Square does not send the bank any evidence that
        /// is removed. Also, you cannot remove evidence after
        /// submitting it to the bank using [SubmitEvidence](https://developer.squareup.com/docs/reference/square/disputes-api/submit-evidence).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to remove evidence from.</param>
        /// <param name="evidenceId">Required parameter: The ID of the evidence you want to remove.</param>
        /// <return>Returns the Models.RemoveDisputeEvidenceResponse response from the API call</return>
        public async Task<Models.RemoveDisputeEvidenceResponse> RemoveDisputeEvidenceAsync(string disputeId, string evidenceId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/evidence/{evidence_id}");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId },
                { "evidence_id", evidenceId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Delete(_queryUrl, _headers, null);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.RemoveDisputeEvidenceResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Returns the specific evidence metadata associated with a specific dispute.
        /// You must maintain a copy of the evidence you upload if you want to
        /// reference it later. You cannot download the evidence
        /// after you upload it.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute that you want to retrieve evidence from.</param>
        /// <param name="evidenceId">Required parameter: The ID of the evidence to retrieve.</param>
        /// <return>Returns the Models.RetrieveDisputeEvidenceResponse response from the API call</return>
        public Models.RetrieveDisputeEvidenceResponse RetrieveDisputeEvidence(string disputeId, string evidenceId)
        {
            Task<Models.RetrieveDisputeEvidenceResponse> t = RetrieveDisputeEvidenceAsync(disputeId, evidenceId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Returns the specific evidence metadata associated with a specific dispute.
        /// You must maintain a copy of the evidence you upload if you want to
        /// reference it later. You cannot download the evidence
        /// after you upload it.
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute that you want to retrieve evidence from.</param>
        /// <param name="evidenceId">Required parameter: The ID of the evidence to retrieve.</param>
        /// <return>Returns the Models.RetrieveDisputeEvidenceResponse response from the API call</return>
        public async Task<Models.RetrieveDisputeEvidenceResponse> RetrieveDisputeEvidenceAsync(string disputeId, string evidenceId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/evidence/{evidence_id}");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId },
                { "evidence_id", evidenceId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Get(_queryUrl,_headers);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.RetrieveDisputeEvidenceResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Uploads a file to use as evidence in a dispute challenge. The endpoint accepts
        /// HTTP multipart/form-data file uploads in HEIC, HEIF, JPEG, PDF, PNG,
        /// and TIFF formats.
        /// For more information, see [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: ID of the dispute you want to upload evidence for.</param>
        /// <param name="request">Optional parameter: Defines parameters for a CreateDisputeEvidenceFile request.</param>
        /// <param name="imageFile">Optional parameter: Example: </param>
        /// <return>Returns the Models.CreateDisputeEvidenceFileResponse response from the API call</return>
        public Models.CreateDisputeEvidenceFileResponse CreateDisputeEvidenceFile(string disputeId, Models.CreateDisputeEvidenceFileRequest request = null, FileStreamInfo imageFile = null)
        {
            Task<Models.CreateDisputeEvidenceFileResponse> t = CreateDisputeEvidenceFileAsync(disputeId, request, imageFile);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Uploads a file to use as evidence in a dispute challenge. The endpoint accepts
        /// HTTP multipart/form-data file uploads in HEIC, HEIF, JPEG, PDF, PNG,
        /// and TIFF formats.
        /// For more information, see [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: ID of the dispute you want to upload evidence for.</param>
        /// <param name="request">Optional parameter: Defines parameters for a CreateDisputeEvidenceFile request.</param>
        /// <param name="imageFile">Optional parameter: Example: </param>
        /// <return>Returns the Models.CreateDisputeEvidenceFileResponse response from the API call</return>
        public async Task<Models.CreateDisputeEvidenceFileResponse> CreateDisputeEvidenceFileAsync(string disputeId, Models.CreateDisputeEvidenceFileRequest request = null, FileStreamInfo imageFile = null, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/evidence_file");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            var requestHeaders = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Content-Type", new [] { "application/json; charset=utf-8" } }
            };

            var imageFileHeaders = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Content-Type", new [] { string.IsNullOrEmpty(imageFile.ContentType) ? "image/jpeg" : imageFile.ContentType } }
            };

            //append form/field parameters
            var _fields = new List<KeyValuePair<string, Object>>()
            {
                new KeyValuePair<string, object>( "image_file", CreateFileMultipartContent(imageFile, imageFileHeaders))
            };
            _fields.Add(new KeyValuePair<string, object>("request", CreateJsonEncodedMultipartContent(request, requestHeaders)));

            //remove null parameters
            _fields = _fields.Where(kvp => kvp.Value != null).ToList();

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Post(_queryUrl, _headers, _fields);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.CreateDisputeEvidenceFileResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Uploads text to use as evidence for a dispute challenge. For more information, see
        /// [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to upload evidence for.</param>
        /// <param name="body">Required parameter: An object containing the fields to POST for the request.  See the corresponding object definition for field details.</param>
        /// <return>Returns the Models.CreateDisputeEvidenceTextResponse response from the API call</return>
        public Models.CreateDisputeEvidenceTextResponse CreateDisputeEvidenceText(string disputeId, Models.CreateDisputeEvidenceTextRequest body)
        {
            Task<Models.CreateDisputeEvidenceTextResponse> t = CreateDisputeEvidenceTextAsync(disputeId, body);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Uploads text to use as evidence for a dispute challenge. For more information, see
        /// [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to upload evidence for.</param>
        /// <param name="body">Required parameter: An object containing the fields to POST for the request.  See the corresponding object definition for field details.</param>
        /// <return>Returns the Models.CreateDisputeEvidenceTextResponse response from the API call</return>
        public async Task<Models.CreateDisputeEvidenceTextResponse> CreateDisputeEvidenceTextAsync(string disputeId, Models.CreateDisputeEvidenceTextRequest body, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/evidence_text");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "content-type", "application/json; charset=utf-8" },
                { "Square-Version", "2020-06-25" }
            };

            //append body params
            var _body = ApiHelper.JsonSerialize(body);

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().PostBody(_queryUrl, _headers, _body);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.CreateDisputeEvidenceTextResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

        /// <summary>
        /// Submits evidence to the cardholder's bank.
        /// Before submitting evidence, Square compiles all available evidence. This includes
        /// evidence uploaded using the
        /// [CreateDisputeEvidenceFile](https://developer.squareup.com/docs/reference/square/disputes-api/create-dispute-evidence-file) and
        /// [CreateDisputeEvidenceText](https://developer.squareup.com/docs/reference/square/disputes-api/create-dispute-evidence-text) endpoints,
        /// and evidence automatically provided by Square, when
        /// available. For more information, see
        /// [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to submit evidence for.</param>
        /// <return>Returns the Models.SubmitEvidenceResponse response from the API call</return>
        public Models.SubmitEvidenceResponse SubmitEvidence(string disputeId)
        {
            Task<Models.SubmitEvidenceResponse> t = SubmitEvidenceAsync(disputeId);
            ApiHelper.RunTaskSynchronously(t);
            return t.Result;
        }

        /// <summary>
        /// Submits evidence to the cardholder's bank.
        /// Before submitting evidence, Square compiles all available evidence. This includes
        /// evidence uploaded using the
        /// [CreateDisputeEvidenceFile](https://developer.squareup.com/docs/reference/square/disputes-api/create-dispute-evidence-file) and
        /// [CreateDisputeEvidenceText](https://developer.squareup.com/docs/reference/square/disputes-api/create-dispute-evidence-text) endpoints,
        /// and evidence automatically provided by Square, when
        /// available. For more information, see
        /// [Challenge a Dispute](https://developer.squareup.com/docs/docs/disputes-api/process-disputes#challenge-a-dispute).
        /// </summary>
        /// <param name="disputeId">Required parameter: The ID of the dispute you want to submit evidence for.</param>
        /// <return>Returns the Models.SubmitEvidenceResponse response from the API call</return>
        public async Task<Models.SubmitEvidenceResponse> SubmitEvidenceAsync(string disputeId, CancellationToken cancellationToken = default)
        {
            //the base uri for api requests
            string _baseUri = config.GetBaseUri();

            //prepare query string for API call
            StringBuilder _queryBuilder = new StringBuilder(_baseUri);
            _queryBuilder.Append("/v2/disputes/{dispute_id}/submit-evidence");

            //process optional template parameters
            ApiHelper.AppendUrlWithTemplateParameters(_queryBuilder, new Dictionary<string, object>()
            {
                { "dispute_id", disputeId }
            });

            //validate and preprocess url
            string _queryUrl = ApiHelper.CleanUrl(_queryBuilder);

            //append request with appropriate headers and parameters
            var _headers = new Dictionary<string, string>()
            { 
                { "user-agent", userAgent },
                { "accept", "application/json" },
                { "Square-Version", "2020-06-25" }
            };

            //prepare the API call request to fetch the response
            HttpRequest _request = GetClientInstance().Post(_queryUrl, _headers, null);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnBeforeHttpRequestEventHandler(GetClientInstance(), _request);
            }

            _request = await authManagers["default"].ApplyAsync(_request).ConfigureAwait(false);

            //invoke request and get response
            HttpStringResponse _response = await GetClientInstance().ExecuteAsStringAsync(_request, cancellationToken).ConfigureAwait(false);
            HttpContext _context = new HttpContext(_request, _response);
            if (HttpCallBack != null)
            {
                HttpCallBack.OnAfterHttpResponseEventHandler(GetClientInstance(), _response);
            }

            //handle errors defined at the API level
            base.ValidateResponse(_response, _context);

            var _responseModel = ApiHelper.JsonDeserialize<Models.SubmitEvidenceResponse>(_response.Body);
            _responseModel.Context = _context;
            return _responseModel;
        }

    }
}