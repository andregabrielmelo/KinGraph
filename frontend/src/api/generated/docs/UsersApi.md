# .UsersApi

All URIs are relative to *https://localhost:7243*

Method | HTTP request | Description
------------- | ------------- | -------------
[**kinGraphWebFeaturesUsersCreateCreateEndpoint**](UsersApi.md#kinGraphWebFeaturesUsersCreateCreateEndpoint) | **POST** /users | Create a new user
[**kinGraphWebFeaturesUsersListListEndpoint**](UsersApi.md#kinGraphWebFeaturesUsersListListEndpoint) | **GET** /users | List users with pagination


# **kinGraphWebFeaturesUsersCreateCreateEndpoint**
> CreateUserResponse kinGraphWebFeaturesUsersCreateCreateEndpoint(createUserRequest)

Creates a new user with the specified name and unit price.

### Example


```typescript
import { createConfiguration, UsersApi } from '';
import type { UsersApiKinGraphWebFeaturesUsersCreateCreateEndpointRequest } from '';

const configuration = createConfiguration();
const apiInstance = new UsersApi(configuration);

const request: UsersApiKinGraphWebFeaturesUsersCreateCreateEndpointRequest = {
    // 
  createUserRequest: {
    name: "Sample User",
    phoneNumber: "phoneNumber_example",
  },
};

const data = await apiInstance.kinGraphWebFeaturesUsersCreateCreateEndpoint(request);
console.log('API called successfully. Returned data:', data);
```


### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **createUserRequest** | **CreateUserRequest**|  |


### Return type

**CreateUserResponse**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
**201** | User created successfully |  -  |
**400** | Invalid request data |  -  |

[[Back to top]](#) [[Back to API list]](README.md#documentation-for-api-endpoints) [[Back to Model list]](README.md#documentation-for-models) [[Back to README]](README.md)

# **kinGraphWebFeaturesUsersListListEndpoint**
> UserListResponse kinGraphWebFeaturesUsersListListEndpoint()

Retrieves a paginated list of all users. Supports GitHub-style pagination with 1-based page indexing and configurable page size.

### Example


```typescript
import { createConfiguration, UsersApi } from '';
import type { UsersApiKinGraphWebFeaturesUsersListListEndpointRequest } from '';

const configuration = createConfiguration();
const apiInstance = new UsersApi(configuration);

const request: UsersApiKinGraphWebFeaturesUsersListListEndpointRequest = {
    // 1-based page index (default 1)
  page: 1,
  
  perPage: 10,
};

const data = await apiInstance.kinGraphWebFeaturesUsersListListEndpoint(request);
console.log('API called successfully. Returned data:', data);
```


### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | [**number**] | 1-based page index (default 1) | defaults to undefined
 **perPage** | [**number**] |  | defaults to undefined


### Return type

**UserListResponse**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
**200** | Paginated list of users returned successfully |  -  |
**400** | Invalid pagination parameters |  -  |

[[Back to top]](#) [[Back to API list]](README.md#documentation-for-api-endpoints) [[Back to Model list]](README.md#documentation-for-models) [[Back to README]](README.md)


