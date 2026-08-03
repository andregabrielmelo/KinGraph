import { RelationshipRecord } from './models/relationship.model';

export function relationshipLabel(relationship: RelationshipRecord): string {
  if (relationship.type === 'Friend') {
    return 'Friend';
  }

  const offset = relationship.generationOffset ?? 0;
  const degree = relationship.degree ?? 0;
  let label = baseFamilyLabel(offset, degree);

  if (relationship.isHalf) {
    label = `Half-${label}`;
  }
  if (relationship.isByMarriage) {
    label = `${label} (by marriage)`;
  }
  return label;
}

function baseFamilyLabel(offset: number, degree: number): string {
  if (offset === 0) {
    return sameGenerationLabel(degree);
  }
  if (degree === 0) {
    return directLineageLabel(offset);
  }
  // Not produced by the current backend factories (only Parent/Sibling/Cousin/Child exist),
  // but kept generic so the UI never shows "undefined" if the domain model grows.
  return `Relative (offset ${offset}, degree ${degree})`;
}

function sameGenerationLabel(degree: number): string {
  // Backend convention: degree=1 => Sibling, degree>=2 => Cousin (ordinal = degree - 1).
  if (degree <= 1) {
    return 'Sibling';
  }
  return `${ordinal(degree - 1)} Cousin`;
}

function directLineageLabel(offset: number): string {
  const steps = Math.abs(offset);
  const ascending = offset < 0;

  const ancestorNames: Record<number, string> = {
    1: 'Parent',
    2: 'Grandparent',
    3: 'Great-Grandparent'
  };
  const descendantNames: Record<number, string> = {
    1: 'Child',
    2: 'Grandchild',
    3: 'Great-Grandchild'
  };

  const table = ascending ? ancestorNames : descendantNames;
  return table[steps] ?? `${steps - 2}x Great-${ascending ? 'Grandparent' : 'Grandchild'}`;
}

function ordinal(n: number): string {
  const suffixes = ['th', 'st', 'nd', 'rd'];
  const v = n % 100;
  return `${n}${suffixes[(v - 20) % 10] ?? suffixes[v] ?? suffixes[0]}`;
}
