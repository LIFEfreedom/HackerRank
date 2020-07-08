namespace determining_dna_health
{
	public class AhoCorasick
	{
		public static Vertex Root = new Vertex(null, '$');

		private Vertex AddVertex(Vertex parent, char smbl)
		{
			Vertex newVertex = new Vertex(parent, smbl);
			parent.AddNextVertex(newVertex);
			return newVertex;
		}

		public void AddGens(string gens, int[] healths)
		{
			uint genNum = 0;
			Vertex vertex = Root;
			foreach (char ch in gens)
			{
				if (ch == ' ')
				{
					vertex.AddGen(genNum, healths[genNum]);
					vertex = Root;
					genNum++;
					continue;
				}

				if (!vertex.NextVertexExist(ch))
				{
					vertex = AddVertex(vertex, ch);
				}
				else
				{
					vertex = vertex.NextVertex(ch);
				}
			}

			vertex.AddGen(genNum, healths[genNum]);
		}

		private Vertex GetSuffLink(Vertex vertex)
		{
			if (vertex.SuffLink is null)
			{
				if (vertex == Root || vertex.Parent == Root)
				{
					vertex.SuffLink = Root;
				}
				else
				{
					vertex.SuffLink = GetAutoMove(GetSuffLink(vertex.Parent), vertex.Symbol);
				}
			}

			return vertex.SuffLink;
		}

		private Vertex GetAutoMove(Vertex vertex, char ch)
		{
			if (!vertex.AutoMoveExist(ch))
			{
				if (vertex.NextVertexExist(ch))
				{
					vertex.AddAutoMove(vertex.NextVertex(ch));
				}
				else
				{
					if (vertex == Root)
					{
						vertex.AddAutoMove(Root, ch);
					}
					else
					{
						vertex.AddAutoMove(GetAutoMove(GetSuffLink(vertex), ch), ch);
					}
				}
			}

			return vertex.AutoMove(ch);
		}

		private Vertex GetGoodSuffLink(Vertex vertex)
		{
			if (vertex.GoodSuffLink is null)
			{
				Vertex suffLink = GetSuffLink(vertex);
				if (suffLink == Root)
				{
					vertex.GoodSuffLink = Root;
				}
				else
				{
					vertex.GoodSuffLink = suffLink.IsGen ? suffLink : GetGoodSuffLink(suffLink);
				}
			}

			return vertex.GoodSuffLink;
		}

		private long Check(Vertex vertex, uint from, uint to)
		{
			long totalGenCost = 0;

			Vertex vrtx = vertex;

			while (vrtx != Root)
			{
				if (vrtx.IsGen)
				{
					totalGenCost += vrtx.GetCost(from, to);
				}

				vrtx = GetGoodSuffLink(vrtx);
			}

			return totalGenCost;
		}

		public long FindAllOccurrences(string dna, uint from, uint to)
		{
			long sum = 0;
			Vertex vertex = Root;

			foreach (char ch in dna)
			{
				vertex = GetAutoMove(vertex, ch);
				sum += Check(vertex, from, to);
			}

			return sum;
		}
	}
}
