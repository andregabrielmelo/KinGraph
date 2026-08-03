import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  ElementRef,
  afterNextRender,
  effect,
  inject,
  input,
  viewChild
} from '@angular/core';
import { DataSet } from 'vis-data';
import { Edge, Network, Node, Options } from 'vis-network';
import { RelationshipRecord } from '../models/relationship.model';
import { relationshipLabel } from '../relationship-label.util';

const GRAPH_OPTIONS: Options = {
  physics: { stabilization: true },
  interaction: { hover: true },
  nodes: { shape: 'ellipse', font: { size: 14 } },
  edges: { font: { size: 11, align: 'top' }, arrows: 'to', smooth: { enabled: true, type: 'dynamic', roundness: 0.5 } }
};

const CENTER_NODE_ID = 'me';

@Component({
  selector: 'app-relationship-graph',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './relationship-graph.html',
  styleUrl: './relationship-graph.css'
})
export class RelationshipGraph {
  readonly myName = input.required<string>();
  readonly items = input.required<readonly RelationshipRecord[]>();

  private readonly containerRef = viewChild.required<ElementRef<HTMLDivElement>>('graphContainer');
  private readonly nodes = new DataSet<Node>();
  private readonly edges = new DataSet<Edge>();
  private network?: Network;

  constructor() {
    // Only ever runs in the browser (never during SSR/prerender), and only after the
    // container <div> has been rendered - exactly what vis-network's `new Network()` needs.
    afterNextRender(() => {
      this.network = new Network(
        this.containerRef().nativeElement,
        { nodes: this.nodes, edges: this.edges },
        GRAPH_OPTIONS
      );
    });

    // Rebuilds the DataSets whenever inputs change. Runs once immediately (before the
    // Network instance exists, which is fine - DataSets are just data), and again on
    // every subsequent input change; vis-network's Network reads live off these DataSets.
    effect(() => {
      const myName = this.myName();
      const items = this.items();

      this.nodes.clear();
      this.edges.clear();

      this.nodes.add({
        id: CENTER_NODE_ID,
        label: myName,
        color: { background: '#2563eb', border: '#1d4ed8' },
        font: { color: '#ffffff' }
      });
      this.nodes.add(items.map((item) => ({ id: item.relatedPersonId, label: item.relatedPersonName })));

      this.edges.add(
        items.map((item, index) => ({
          id: index,
          from: CENTER_NODE_ID,
          to: item.relatedPersonId,
          label: relationshipLabel(item),
          dashes: item.type === 'Friend'
        }))
      );
    });

    inject(DestroyRef).onDestroy(() => this.network?.destroy());
  }
}
