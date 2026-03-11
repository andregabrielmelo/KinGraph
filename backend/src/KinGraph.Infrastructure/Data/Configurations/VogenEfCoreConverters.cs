using KinGraph.Core.UserAggregate;
using Vogen;

namespace KinGraph.Infrastructure.Data.Configurations;

[EfCoreConverter<UserId>]
[EfCoreConverter<UserName>]
internal partial class VogenEfCoreConverters;
