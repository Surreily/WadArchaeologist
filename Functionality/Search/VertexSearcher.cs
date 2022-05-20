using System;
using System.Collections.Generic;
using System.Linq;
using Surreily.WadArchaeologist.Functionality.Context;
using Surreily.WadArchaeologist.Functionality.Helpers;
using Surreily.WadArchaeologist.Functionality.Model;
using Surreily.WadArchaeologist.Model;

namespace Surreily.WadArchaeologist.Functionality.Search {
    public class VertexSearcher {
        public void Search(SearchOptions options, Wad wad) {
            wad.VertexLists = new List<List<Vertex>>();

            foreach (List<Line> lines in wad.LineLists) {
                TryFindVerticesForLines(wad, lines);
            }
        }

        private void TryFindVerticesForLines(Wad wad, List<Line> lines) {
            int vertexCount = lines
                .Max(l => Math.Max(l.StartVertexId, l.EndVertexId));

            foreach (DataRegion region in wad.UnallocatedRegions.ToList()) {
                int position = region.Position;

                while (position < (region.Position + region.Length) - (lines.Count * 4)) {
                    if (!GetAreAnyVerticesDuplicates(wad, position, vertexCount)) {
                        if (TryFindVertices(wad, lines, position)) {
                            List<Vertex> vertices = new List<Vertex>();

                            for (int i = 0; i < vertexCount; i++) {
                                vertices.Add(new Vertex {
                                    X = wad.Data.ReadShort(position + (i * 4)),
                                    Y = wad.Data.ReadShort(position + (i * 4) + 2),
                                });
                            }

                            wad.VertexLists.Add(vertices);

                            return;
                        }
                    }

                    position++;
                }
            }
        }

        private bool GetAreAnyVerticesDuplicates(Wad wad, int position, int vertexCount) {
            IList<Tuple<short, short>> vertices = new List<Tuple<short, short>>();

            float distance = 0;

            for (int i = 0; i < vertexCount; i++) {
                short x = wad.Data.ReadShort(position + (i * 4));
                short y = wad.Data.ReadShort(position + (i * 4) + 2);

                vertices.Add(Tuple.Create(x, y));

                for (int j = 0; j < i; j++) {
                    short oldX = vertices[j].Item1;
                    short oldY = vertices[j].Item2;

                    if (x == oldX && y == oldY) {
                        return true;
                    }

                    checked {
                        distance += MathHelper.GetDistance(x, y, oldX, oldY);
                    }
                }
            }

            float averageDistance = distance / (((float)vertexCount * (float)vertexCount / 2f) - (float)vertexCount / 2f);

            if (averageDistance < 6000f) {
                return false;
            }

            return true;
        }

        private bool TryFindVertices(Wad wad, List<Line> lines, int position) {
            for (int i = 1; i < lines.Count; i++) {
                for (int j = 0; j < i; j++) {
                    short aX = wad.Data.ReadShort(position + lines[i].StartVertexId);
                    short aY = wad.Data.ReadShort(position + lines[i].StartVertexId + 2);
                    short bX = wad.Data.ReadShort(position + lines[i].EndVertexId);
                    short bY = wad.Data.ReadShort(position + lines[i].EndVertexId + 2);
                    short cX = wad.Data.ReadShort(position + lines[j].StartVertexId);
                    short cY = wad.Data.ReadShort(position + lines[j].StartVertexId + 2);
                    short dX = wad.Data.ReadShort(position + lines[j].EndVertexId);
                    short dY = wad.Data.ReadShort(position + lines[j].EndVertexId + 2);

                    if (MathHelper.DoLinesIntersect(aX, aY, bX, bY, cX, cY, dX, dY)) {
                        return false;
                    }

                    if (j > 20) {
                        Console.WriteLine("Test");
                    }
                }
            }

            return true;
        }
    }
}
