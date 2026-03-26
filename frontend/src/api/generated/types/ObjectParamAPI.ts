import { ResponseContext, RequestContext, HttpFile, HttpInfo } from '../http/http';
import { Configuration, ConfigurationOptions } from '../configuration'
import type { Middleware } from '../middleware';

import { CreateUserRequest } from '../models/CreateUserRequest';
import { CreateUserResponse } from '../models/CreateUserResponse';
import { PagedResultOfUserRecord } from '../models/PagedResultOfUserRecord';
import { ProblemDetails } from '../models/ProblemDetails';
import { UserListResponse } from '../models/UserListResponse';
import { UserRecord } from '../models/UserRecord';

import { ObservableUsersApi } from "./ObservableAPI";
import { UsersApiRequestFactory, UsersApiResponseProcessor} from "../apis/UsersApi";

export interface UsersApiKinGraphWebFeaturesUsersCreateCreateEndpointRequest {
    /**
     * 
     * @type CreateUserRequest
     * @memberof UsersApikinGraphWebFeaturesUsersCreateCreateEndpoint
     */
    createUserRequest: CreateUserRequest
}

export interface UsersApiKinGraphWebFeaturesUsersListListEndpointRequest {
    /**
     * 1-based page index (default 1)
     * Defaults to: undefined
     * @type number
     * @memberof UsersApikinGraphWebFeaturesUsersListListEndpoint
     */
    page: number
    /**
     * 
     * Defaults to: undefined
     * @type number
     * @memberof UsersApikinGraphWebFeaturesUsersListListEndpoint
     */
    perPage: number
}

export class ObjectUsersApi {
    private api: ObservableUsersApi

    public constructor(configuration: Configuration, requestFactory?: UsersApiRequestFactory, responseProcessor?: UsersApiResponseProcessor) {
        this.api = new ObservableUsersApi(configuration, requestFactory, responseProcessor);
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param param the request object
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(param: UsersApiKinGraphWebFeaturesUsersCreateCreateEndpointRequest, options?: ConfigurationOptions): Promise<HttpInfo<CreateUserResponse>> {
        return this.api.kinGraphWebFeaturesUsersCreateCreateEndpointWithHttpInfo(param.createUserRequest,  options).toPromise();
    }

    /**
     * Creates a new user with the specified name and unit price.
     * Create a new user
     * @param param the request object
     */
    public kinGraphWebFeaturesUsersCreateCreateEndpoint(param: UsersApiKinGraphWebFeaturesUsersCreateCreateEndpointRequest, options?: ConfigurationOptions): Promise<CreateUserResponse> {
        return this.api.kinGraphWebFeaturesUsersCreateCreateEndpoint(param.createUserRequest,  options).toPromise();
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param param the request object
     */
    public kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(param: UsersApiKinGraphWebFeaturesUsersListListEndpointRequest, options?: ConfigurationOptions): Promise<HttpInfo<UserListResponse>> {
        return this.api.kinGraphWebFeaturesUsersListListEndpointWithHttpInfo(param.page, param.perPage,  options).toPromise();
    }

    /**
     * Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.
     * List users with pagination
     * @param param the request object
     */
    public kinGraphWebFeaturesUsersListListEndpoint(param: UsersApiKinGraphWebFeaturesUsersListListEndpointRequest, options?: ConfigurationOptions): Promise<UserListResponse> {
        return this.api.kinGraphWebFeaturesUsersListListEndpoint(param.page, param.perPage,  options).toPromise();
    }

}
