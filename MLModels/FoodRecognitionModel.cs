using Microsoft.ML;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms;
using System.IO;
using System.Drawing;
using Microsoft.ML.TensorFlow;
using AgrosSenseAPI.Services;

public class FoodRecognitionModel
{
    private readonly MLContext _mlContext;

    public FoodRecognitionModel()
    {
        _mlContext = new MLContext();
    }

    public void TrainModel(string dataPath, string modelPath)
    {
        var dataView = _mlContext.Data.LoadFromTextFile<FoodData>(dataPath, separatorChar: ',', hasHeader: true);

        var tensorFlowModel = _mlContext.Model.LoadTensorFlowModel(modelPath);
        var schema = tensorFlowModel.GetInputSchema();

        var pipeline = _mlContext.Transforms.LoadImages("ImagePath", null, nameof(FoodData.ImagePath))
            .Append(_mlContext.Transforms.ResizeImages("ImagePath", 224, 224, nameof(FoodData.ImagePath)));
             
              




        var model = pipeline.Fit(dataView);

        // Salvar o modelo treinado
        _mlContext.Model.Save(model, dataView.Schema, "FoodRecognitionModel.zip");
    }
}
