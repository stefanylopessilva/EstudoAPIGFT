using System.Collections.Generic;

namespace CinemaAPI.HATEOAS
{
    public class HATEOAS
    {
        private string url;
        private string protocol = "https://";
        public List<Link> actions = new List<Link>();

        public HATEOAS(string url)
        {
            this.url = url;
        }
        public HATEOAS(string url, string protocol)
        {
            this.url = url;
            this.protocol = protocol;
        }
        public void AddAction(string rel, string method)
        {
            actions.Add(new Link(this.protocol + this.url, rel, method));
        }
        public Link[] GetActions(string sufixo)
        {
            Link[] tempLinks = new Link[actions.Count];

            for (int i = 0; i < tempLinks.Length; i++)
            {
                tempLinks[i] = new Link(actions[i].Href, actions[i].Rel, actions[i].Method);
            }

            foreach (var link in tempLinks)
            {
                link.Href = link.Href + "/" + sufixo;
            }
            return tempLinks;
        }
    }
}