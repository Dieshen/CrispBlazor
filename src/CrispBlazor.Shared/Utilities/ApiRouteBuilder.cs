using System.Text;

namespace CrispBlazor.Shared.Utilities
{
    /// <summary>
    /// This class is used to give a name to the API verbs. It is used to identify the action that is being performed on the object.
    /// </summary>
    public record ApiVerb
    {
        public static ApiVerb Archive => new(nameof(Archive));
        public static ApiVerb Download => new(nameof(Download));
        public static ApiVerb Upload => new(nameof(Upload));
        public static ApiVerb Move => new(nameof(Move));
        public static ApiVerb Copy => new(nameof(Copy));

        public ApiVerb(string name) { Name = name; }
        public string Name { get; private init; } = string.Empty;
    }

    /// <summary>
    /// This class is used to build API routes. It allows you to set the group name, Id, version, and query variables.
    /// </summary>
    public sealed class ApiRouteBuilder
    {
        private const string StartQuery = "?";
        private const string Separator = "&";

        private string _groupName = "";
        private Guid? _Id;
        private int _majorVersion = 1, _minorVersion = 0;
        private List<string> _queryVariables = [];
        private ApiVerb? _verb = null;

        /// <summary>
        /// Builds the route based on the parameters set in the builder.
        /// </summary>
        /// <returns>Built route</returns>
        public string Build()
        {
            var route = new StringBuilder("api/");

            if (!string.IsNullOrEmpty(_groupName))
                route.Append($"{_groupName}/");

            if (_Id is not null)
                route.Append($"{_Id.Value}/");

            if (_verb is not null)
                route.Append(_verb.Name);

            route.Append(StartQuery);
            route.Append(GetVersion());

            _queryVariables.ForEach(v =>
            {
                route.Append(Separator);
                route.Append(v);
            });

            return route.ToString();
        }

        /// <summary>
        /// Sets the group name of the object. This is used to identify the object in the route.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public ApiRouteBuilder WithGroupName(string groupName)
        {
            _groupName = groupName;
            return this;
        }

        /// <summary>
        /// Sets the Id of the object. This is used to identify the object in the route.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiRouteBuilder WithId(Guid? id)
        {
            _Id = id;
            return this;
        }

        /// <summary>
        /// Sets the verb of the object. This is used to identify the action that is being performed on the object.
        /// </summary>
        /// <param name="verb"></param>
        /// <returns></returns>
        public ApiRouteBuilder WithVerb(ApiVerb verb)
        {
            _verb = verb;
            return this;
        }

        /// <summary>
        /// Sets the version of the API. The default is 1.0.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>
        public ApiRouteBuilder WithVersion(int major, int minor)
        {
            _majorVersion = major;
            _minorVersion = minor;
            return this;
        }

        /// <summary>
        /// Adds query variables to the route. Should be formatted as "key=value".
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        public ApiRouteBuilder AddQueryVariables(params string[] variables)
        {
            _queryVariables.AddRange(variables);
            return this;
        }

        private string GetVersion() =>
            $"api-version={_majorVersion}.{_minorVersion};";

    }
}
