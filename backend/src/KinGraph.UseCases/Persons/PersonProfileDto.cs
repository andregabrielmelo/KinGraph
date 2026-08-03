using KinGraph.Core.Enumerations;

namespace KinGraph.UseCases.Persons;

public record PersonProfileDto(
    string Name,
    Gender? Gender,
    DateTime? DateOfBirth,
    string? OccupationName,
    decimal? OccupationSalary
);
