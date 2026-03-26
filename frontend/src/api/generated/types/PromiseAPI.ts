import { ResponseContext, RequestContext, HttpFile, HttpInfo } from '../http/http';
import { Configuration, PromiseConfigurationOptions, wrapOptions } from '../configuration'
import { PromiseMiddleware, Middleware, PromiseMiddlewareWrapper } from '../middleware';

import { CreateUserRequest } from '../models/CreateUserRequest';
import { CreateUserResponse } from '../models/CreateUserResponse';
import { PagedResultOfUserRecord } from '../models/PagedResultOfUserRecord';
import { ProblemDetails } from '../models/ProblemDetails';
import { UserListResponse } from '../models/UserListResponse';
import { UserRecord } from '../models/UserRecord';
import { ObservableUsersApi } from './ObservableAPI';

import { UsersApiRequestFactory, UsersApiResponseProcessor} from "../apis/UsersApi";
export class PromiseUsersApi {
    private api: ObservableUsersApi

    public constructor(
        configuration: Configuration,
        requestFactory?: UsersApiRequestFactory,
        responseProcessor?: UsersApiResponseProcessor
    ) {
        this.api = new ObservableUsersApi(configuration, requestFactory, responseProcessor);
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param createUserRequest 
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(createUserRequest: CreateUserRequest, _options?: PromiseConfigurationOptions): Promise<HttpInfo<CreateUserResponse>> {
        const observableOptions = wrapOptions(_options);
        const result = this.api.kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(createUserRequest, observableOptions);
        return result.toPromise();
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param createUserRequest 
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpoint(createUserRequest: CreateUserRequest, _options?: PromiseConfigurationOptions): Promise<CreateUserResponse> {
        const observableOptions = wrapOptions(_options);
        const result = this.api.kinGraphWebFeaturesUsersCreateCreateEndpoint(createUserRequest, observableOptions);
        return result.toPromise();
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param page 1-based page index (default 1)
     * @param perPage
     */
    public kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(page: number, perPage: number, _options?: PromiseConfigurationOptions): Promise<HttpInfo<UserListResponse>> {
        const observableOptions = wrapOptions(_options);
        const result = this.api.kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(page, perPage, observableOptions);
        return result.toPromise();
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param page 1-based page index (default 1)
     * @param perPage
     */
    public kinGraphWebFeaturesUsersListListEndpoint(page: number, perPage: number, _options?: PromiseConfigurationOptions): Promise<UserListResponse> {
        const observableOptions = wrapOptions(_options);
        const result = this.api.kinGraphWebFeaturesUsersListListEndpoint(page, perPage, observableOptions);
        return result.toPromise();
    }


}



