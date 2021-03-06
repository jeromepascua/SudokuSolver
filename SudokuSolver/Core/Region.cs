﻿using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    internal enum SudokuRegion
    {
        Row,
        Column,
        Block
    }

    internal class Region
    {
        internal readonly SPoint[] Points;
        internal readonly Cell[] Cells;

        internal Region(Puzzle puzzle, SudokuRegion region, int index)
        {
            switch (region)
            {
                case SudokuRegion.Block:
                    Points = new SPoint[9];
                    int ix = (index % 3) * 3, iy = (index / 3) * 3;
                    int c = 0;
                    for (int x = ix; x < ix + 3; x++)
                    {
                        for (int y = iy; y < iy + 3; y++)
                        {
                            Points[c++] = new SPoint(x, y);
                        }
                    }
                    break;
                case SudokuRegion.Row:
                    Points = new SPoint[9];
                    for (int i = 0; i < 9; i++)
                        Points[i] = new SPoint(i, index);
                    break;
                case SudokuRegion.Column:
                    Points = new SPoint[9];
                    for (int i = 0; i < 9; i++)
                        Points[i] = new SPoint(index, i);
                    break;
            }
            Cells = Points.Select(p => puzzle[p]).ToArray();
        }

        internal int[] GetRegion() => Cells.Select(c => c.Value).ToArray();
        internal HashSet<int>[] GetCandidates() => Cells.Select(c => c.Candidates).ToArray();

        internal Cell[] GetCellsWithCandidates(params int[] values) => Cells.Where(c => c.Candidates.ContainsAll(values)).ToArray();
        internal SPoint[] GetPointsWithCandidates(params int[] values) => GetCellsWithCandidates(values).Select(c => c.Point).ToArray();
    }
}
