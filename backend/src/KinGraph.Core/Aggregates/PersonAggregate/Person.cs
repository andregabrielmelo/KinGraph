using KinGraph.Core.Enumerations;

namespace KinGraph.Core.Aggregates.PersonAggregate;

public class Person(PersonName name) : EntityBase<Person, PersonId>, IAggregateRoot
{
    private readonly List<Relationship> _relationships = [];
    private readonly List<Address> _addresses = [];
    private readonly List<SocialProfile> _socialMediaProfiles = [];
    private readonly List<Language> _languages = [];
    private readonly List<Hobby> _hobbies = [];
    private readonly List<Citizenship> _citizenships = [];
    private readonly List<GovermentalDocument> _governmentalDocuments = [];
    private readonly List<ExtraField> _extraFields = [];

    public PersonName Name { get; private set; } = name;
    public Gender? Gender { get; private set; }
    public MaritalStatus? MaritalStatus { get; private set; }
    public Height? Height { get; private set; }
    public Weight? Weight { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public EmailAddress? EmailAddress { get; private set; }
    public DateOfBirth? DateOfBirth { get; private set; }
    public BloodType? BloodType { get; private set; }
    public Occupation? Occupation { get; private set; }
    public Country? Nationality { get; private set; }
    public Address? PlaceOfBirth { get; private set; }
    public IReadOnlyCollection<Relationship> Relationships => _relationships.AsReadOnly();
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    public IReadOnlyCollection<SocialProfile> SocialMediaProfiles =>
        _socialMediaProfiles.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
    public IReadOnlyCollection<Hobby> Hobbies => _hobbies.AsReadOnly();
    public IReadOnlyCollection<Citizenship> Citizenships => _citizenships.AsReadOnly();
    public IReadOnlyCollection<GovermentalDocument> GovernmentalDocuments =>
        _governmentalDocuments.AsReadOnly();
    public IReadOnlyCollection<ExtraField> ExtraFields => _extraFields.AsReadOnly();

    public static Person Create(PersonName name) => new Person(name);

    public Person UpdateName(PersonName newName)
    {
        if (newName == null)
            throw new ArgumentNullException(nameof(newName));
        if (Name != newName)
            Name = newName;
        return this;
    }

    public Person UpdateGender(Gender newGender)
    {
        Gender = newGender;
        return this;
    }

    public Person UpdateMaritalStatus(MaritalStatus newStatus)
    {
        MaritalStatus = newStatus;
        return this;
    }

    public Person UpdateHeight(Height newHeight)
    {
        Height = newHeight ?? throw new ArgumentNullException(nameof(newHeight));
        return this;
    }

    public Person UpdateWeight(Weight newWeight)
    {
        Weight = newWeight ?? throw new ArgumentNullException(nameof(newWeight));
        return this;
    }

    public Person UpdatePhoneNumber(PhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
        return this;
    }

    public Person UpdateEmailAddress(EmailAddress newEmailAddress)
    {
        EmailAddress = newEmailAddress;
        return this;
    }

    public Person UpdateDateOfBirth(DateOfBirth newDateOfBirth)
    {
        DateOfBirth = newDateOfBirth ?? throw new ArgumentNullException(nameof(newDateOfBirth));
        return this;
    }

    public Person UpdateBloodType(BloodType newBloodType)
    {
        BloodType = newBloodType;
        return this;
    }

    public Person UpdateOccupation(Occupation newOccupation)
    {
        Occupation = newOccupation;
        return this;
    }

    public Person UpdateNationality(Country newNationality)
    {
        Nationality = newNationality;
        return this;
    }

    public Person UpdatePlaceOfBirth(Address newPlaceOfBirth)
    {
        PlaceOfBirth = newPlaceOfBirth;
        return this;
    }

    public Person AddRelationship(Relationship relationship)
    {
        if (!Relationships.Contains(relationship))
            _relationships.Add(relationship);
        return this;
    }

    public Person RemoveRelationship(Relationship relationship)
    {
        _relationships.Remove(relationship);
        return this;
    }

    public Person AddAddress(Address address)
    {
        if (!_addresses.Contains(address))
            _addresses.Add(address);
        return this;
    }

    public Person RemoveAddress(Address address)
    {
        _addresses.Remove(address);
        return this;
    }

    public Person AddLanguage(Language language)
    {
        if (!_languages.Contains(language))
            _languages.Add(language);
        return this;
    }

    public Person RemoveLanguage(Language language)
    {
        _languages.Remove(language);
        return this;
    }

    public Person AddHobby(Hobby hobby)
    {
        if (!_hobbies.Contains(hobby))
            _hobbies.Add(hobby);
        return this;
    }

    public Person RemoveHobby(Hobby hobby)
    {
        _hobbies.Remove(hobby);
        return this;
    }

    public Person AddCitizenship(Citizenship citizenship)
    {
        if (!_citizenships.Contains(citizenship))
            _citizenships.Add(citizenship);
        return this;
    }

    public Person RemoveCitizenship(Citizenship citizenship)
    {
        _citizenships.Remove(citizenship);
        return this;
    }

    public Person AddGovermentalDocument(GovermentalDocument document)
    {
        if (!_governmentalDocuments.Contains(document))
            _governmentalDocuments.Add(document);
        return this;
    }

    public Person RemoveGovermentalDocument(GovermentalDocument document)
    {
        _governmentalDocuments.Remove(document);
        return this;
    }

    public Person AddSocialProfile(SocialProfile profile)
    {
        if (!_socialMediaProfiles.Contains(profile))
            _socialMediaProfiles.Add(profile);
        return this;
    }

    public Person RemoveSocialProfile(SocialProfile profile)
    {
        _socialMediaProfiles.Remove(profile);
        return this;
    }

    public Person AddExtraField(ExtraField field)
    {
        if (!_extraFields.Contains(field))
            _extraFields.Add(field);
        return this;
    }

    public Person RemoveExtraField(ExtraField field)
    {
        _extraFields.Remove(field);
        return this;
    }
}
