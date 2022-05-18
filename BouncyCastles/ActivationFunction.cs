using System;
using System.Collections.Generic;

namespace BouncyCastles.ActivationFunctions
{
    /// <summary>
    /// This class acts as a base class to activation functions as well as a cache for common and custom activation functions accessible at runtime
    /// 
    /// Yes, I probably should have split that into 2 separate classes, but this is demoware, wotcha gonna do?
    /// </summary>
    public abstract class ActivationFunction
    {
        private static Dictionary<ActivationFunctionType, ActivationFunction> _functions;
        private static Dictionary<string, ActivationFunction> _customFunctions;

        public enum ActivationFunctionType { SoftPlus, Relu, Sigmoid, Custom }

        static ActivationFunction()
        {
            Load();
        }

        protected ActivationFunction()
        {
        }

        //Base functionality for subclasses


        //Static Builder functionality
        public static ActivationFunction Get(ActivationFunctionType activationFunctionType)
        {
            if (activationFunctionType == ActivationFunctionType.Custom)
                throw new InvalidOperationException("Please use Build() or GetCustom() methods for custom activation functions");

            ActivationFunction function = null;

            if (_functions.TryGetValue(activationFunctionType, out function))
            {
                return function;
            }

            throw new InvalidOperationException($"Function {activationFunctionType} not available");
        }

        /// <summary>
        /// Add a custom calculation to the internal cache, retrievable by supplied tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="function"></param>
        public static void Add(string tag, Func<decimal, decimal> function) => _customFunctions[tag] = function;

        private static void Load()
        {
            _customFunctions = new Dictionary<string, ActivationFunction>();
            _functions = new Dictionary<ActivationFunctionType, ActivationFunction>();
        }

        private static void Add<TActivationFunction>() where TActivationFunction : ActivationFunction
        {

        }

        
    }
}