﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2020 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Emgu.CV
{
    /// <summary>
    /// Native implementation to read files into Mat or Images.
    /// </summary>
    public static class FileReader
    {
        private static Emgu.CV.IFileReaderMat[] _fileReaderMatArr;
        //private static Emgu.CV.IFileReaderImage[] _fileReaderImageArr;

        /// <summary>
        /// Read a file into Mat using native implementations
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <param name="mat">The Mat to read the file into</param>
        /// <param name="loadType">The image load type.</param>
        /// <returns>True if successful</returns>
        public static bool ReadFileToMat(String fileName, Mat mat, CvEnum.ImreadModes loadType)
        {
            if (_fileReaderMatArr == null)
            {
                Type[] readersTypes = Emgu.Util.Toolbox.GetIntefaceImplementationFromAssembly<Emgu.CV.IFileReaderMat>();
                Emgu.CV.IFileReaderMat[] matArr = new IFileReaderMat[readersTypes.Length];
                for (int i = 0; i < readersTypes.Length; i++)
                {
                    matArr[i] = Activator.CreateInstance(readersTypes[i]) as Emgu.CV.IFileReaderMat;
                }

                _fileReaderMatArr = matArr;
            }

            foreach (IFileReaderMat reader in _fileReaderMatArr)
            {
                if (reader.ReadFile(fileName, mat, loadType))
                    return true;
            }

            return false;
        }

        /*
        public static bool ReadFileToImage<TColor, TDepth>(String fileName, Image<TColor, TDepth> image)
            where TColor : struct, IColor
            where TDepth : new()
        {
            if (_fileReaderImageArr == null)
            {
                Type[] readersTypes = Emgu.Util.Toolbox.GetIntefaceImplementationFromAssembly<Emgu.CV.IFileReaderImage>();
                Emgu.CV.IFileReaderImage[] imageArr = new IFileReaderImage[readersTypes.Length];
                for (int i = 0; i < readersTypes.Length; i++)
                {
                    imageArr[i] = Activator.CreateInstance(readersTypes[i]) as Emgu.CV.IFileReaderImage;
                }

                _fileReaderImageArr = imageArr;
            }

            foreach (IFileReaderImage reader in _fileReaderImageArr)
            {
                if (reader.ReadFile(fileName, image))
                    return true;
            }
            
            return false;
        }
        */
    }
}
