using KinGraph.Core.Aggregates.PersonAggregate;
using KinGraph.Core.Aggregates.UserAggregate;
using Vogen;

namespace KinGraph.Infrastructure.Data.Configurations;

[EfCoreConverter<UserId>]
[EfCoreConverter<UserName>]
[EfCoreConverter<PersonId>]
[EfCoreConverter<PersonName>]
internal partial class VogenEfCoreConverters;
