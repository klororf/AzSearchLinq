
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace test.Models;
public class Product{
        [SimpleField(IsFilterable =true,IsKey =true)]
        public string ProductName { get; set; }
        
        [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
        public string Description {get;set;}
        [SimpleField(IsFilterable =true)]
        public double Price { get; set; }

        [SearchableField(IsFilterable =true)]
        public string[] Color { get; set; }

        [SearchableField(IsFilterable =true)]
        public string Material { get; set; }

        [SearchableField]
        public string Size { get; set; }
}