export interface RelationshipRecord {
  type: 'Family' | 'Friend';
  relatedPersonId: number;
  relatedPersonName: string;
  generationOffset: number | null;
  degree: number | null;
  isByMarriage: boolean | null;
  isHalf: boolean | null;
}

export type FamilyRelationshipKind = 'Parent' | 'Sibling' | 'Cousin';

export interface CreateRelationshipRequest {
  relatedUserId: number;
  type: 'Family' | 'Friend';
  kind?: FamilyRelationshipKind;
  cousinDegree?: number;
  isByMarriage: boolean;
  isHalf: boolean;
}

export interface UserSummary {
  id: number;
  name: string;
}
