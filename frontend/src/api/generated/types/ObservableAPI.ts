import { ResponseContext, RequestContext, HttpFile, HttpInfo } from '../http/http';
import { Configuration, ConfigurationOptions, mergeConfiguration } from '../configuration'
import type { Middleware } from '../middleware';
import { Observable, of, from } from '../rxjsStub';
import {mergeMap, map} from  '../rxjsStub';
import { CreateUserRequest } from '../models/CreateUserRequest';
import { CreateUserResponse } from '../models/CreateUserResponse';
import { PagedResultOfUserRecord } from '../models/PagedResultOfUserRecord';
import { ProblemDetails } from '../models/ProblemDetails';
import { UserListResponse } from '../models/UserListResponse';
import { UserRecord } from '../models/UserRecord';

import { UsersApiRequestFactory, UsersApiResponseProcessor} from "../apis/UsersApi";
export class ObservableUsersApi {
    private requestFactory: UsersApiRequestFactory;
    private responseProcessor: UsersApiResponseProcessor;
    private configuration: Configuration;

    public constructor(
        configuration: Configuration,
        requestFactory?: UsersApiRequestFactory,
        responseProcessor?: UsersApiResponseProcessor
    ) {
        this.configuration = configuration;
        this.requestFactory = requestFactory || new UsersApiRequestFactory(configuration);
        this.responseProcessor = responseProcessor || new UsersApiResponseProcessor();
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param createUserRequest 
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(createUserRequest: CreateUserRequest, _options?: ConfigurationOptions): Observable<HttpInfo<CreateUserResponse>> {
        const _config = mergeConfiguration(this.configuration, _options);

        const requestContextPromise = this.requestFactory.kinGraphWebFeaturesUsersCreateCreateEndpoint(createUserRequest, _config);
        // build promise chain
        let middlewarePreObservable = from<RequestContext>(requestContextPromise);
        for (const middleware of _config.middleware) {
            middlewarePreObservable = middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => middleware.pre(ctx)));
        }

        return middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => _config.httpApi.send(ctx))).
            pipe(mergeMap((response: ResponseContext) => {
                let middlewarePostObservable = of(response);
                for (const middleware of _config.middleware.reverse()) {
                    middlewarePostObservable = middlewarePostObservable.pipe(mergeMap((rsp: ResponseContext) => middleware.post(rsp)));
                }
                return middlewarePostObservable.pipe(map((rsp: ResponseContext) => this.responseProcessor.kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(rsp)));
            }));
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param createUserRequest 
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpoint(createUserRequest: CreateUserRequest, _options?: ConfigurationOptions): Observable<CreateUserResponse> {
        return this.kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(createUserRequest, _options).pipe(map((apiResponse: HttpInfo<CreateUserResponse>) => apiResponse.data));
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param page 1-based page index (default 1)
     * @param perPage
     */
    public kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(page: number, perPage: number, _options?: ConfigurationOptions): Observable<HttpInfo<UserListResponse>> {
        const _config = mergeConfiguration(this.configuration, _options);

        const requestContextPromise = this.requestFactory.kinGraphWebFeaturesUsersListListEndpoint(page, perPage, _config);
        // build promise chain
        let middlewarePreObservable = from<RequestContext>(requestContextPromise);
        for (const middleware of _config.middleware) {
            middlewarePreObservable = middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => middleware.pre(ctx)));
        }

        return middlewarePreObservable.pipe(mergeMap((ctx: RequestContext) => _config.httpApi.send(ctx))).
            pipe(mergeMap((response: ResponseContext) => {
                let middlewarePostObservable = of(response);
                for (const middleware of _config.middleware.reverse()) {
                    middlewarePostObservable = middlewarePostObservable.pipe(mergeMap((rsp: ResponseContext) => middleware.post(rsp)));
                }
                return middlewarePostObservable.pipe(map((rsp: ResponseContext) => this.responseProcessor.kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(rsp)));
            }));
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param page 1-based page index (default 1)
     * @param perPage
     */
    public kinGraphWebFeaturesUsersListListEndpoint(page: number, perPage: number, _options?: ConfigurationOptions): Observable<UserListResponse> {
        return this.kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(page, perPage, _options).pipe(map((apiResponse: HttpInfo<UserListResponse>) => apiResponse.data));
    }

}
