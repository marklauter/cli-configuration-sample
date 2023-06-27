using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CLI.Config.Sample
{
    public interface IOptionsWriter
    {
        void WriteOptions();
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOptionsWriter(this IServiceCollection services)
        {
            services.TryAddTransient<IOptionsWriter, OptionsWriter>();
            return services;
        }
    }

    sealed file class OptionsWriter
        : IOptionsWriter
    {
        private readonly SampleOptions sampleOptions;

        public OptionsWriter(IOptions<SampleOptions> options)
        {
            if (options is null || options.Value is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.sampleOptions = options.Value;
        }

        public void WriteOptions()
        {
            Console.WriteLine(JsonConvert.SerializeObject(this.sampleOptions, Formatting.Indented));
        }
    }
}
