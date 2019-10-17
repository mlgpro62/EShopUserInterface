﻿using ApiHelperLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiHelperLibrary.Processors
{
    public class ImageProcessor : Processor
    {
        public static async Task<List<ImageModel>> GetImagesByProductId(int id = 1)
        {
            using (HttpResponseMessage response = await ApiHelper.apiClient.GetAsync(LinkGetProductImagesByProductID(id)))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<ImageModel>>();
                }
                throw new Exception("Unexpected error in ImageProcessor");
            }
        }

        public static async Task SaveImage(string ImagePath, int ProductID)
        {
            using (HttpResponseMessage response = await ApiHelper.apiClient.PostAsJsonAsync<SingleImageModel>(LinkUploadImage(ProductID), new SingleImageModel() { Image = ImageModel.GetImageBytesFromDirectory(ImagePath), FileExtension = Path.GetExtension(ImagePath) }))
            {
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new Exception($"Unexpected error in ImageProcessor \nResponse code: {response.StatusCode}");
            }
        }
    }
}
