using System;
using System.Collections.Generic;

namespace BouncyCastles.ActivationFunctions
{
    /// <summary>
    /// This class acts as a base class to activation functions as well as a cache for common and custom activation functions accessible at runtime
    /// 
    /// Yes, I probably should have split that into 2 separate classes, but this is demoware, wotcha gonna do :shrug:?
    /// </summary>
    public abstract class ActivationFunction
    {
        //Base functionality for subclasses        
        public enum ActivationFunctionType { SoftPlus, Relu, Sigmoid, Custom }
        public ActivationFunctionType FunctionType { get; private set; }

        Func<double, double> _function;

        protected ActivationFunction(Func<double, double> function, ActivationFunctionType type)
        {
            FunctionType = type;
            _function = function;
        }

        public double Evaluate(double input) => _function(input);

        //Static Builder functionality
        private static Dictionary<ActivationFunctionType, ActivationFunction> _functions;
        private static Dictionary<string, CustomActivationFunction> _customFunctions;

        static ActivationFunction()
        {
            Load();
        }

        /// <summary>
        /// Get a defined activation function type
        /// </summary>
        /// <param name="activationFunctionType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
        public static void Add(string tag, Func<double, double> function) => _customFunctions[tag] = new CustomActivationFunction(tag, function);

        private static void Load()
        {
            _customFunctions = new Dictionary<string, CustomActivationFunction>();
            _functions = new Dictionary<ActivationFunctionType, ActivationFunction>();

            Add<SoftplusActivationFunction>();
            Add<ReluActivationFunction>();
            Add<SigmoidActivationFunction>();
        }

        private static void Add<TActivationFunction>() where TActivationFunction : ActivationFunction
        {
            ActivationFunction function = Activator.CreateInstance<TActivationFunction>();

            _functions[function.FunctionType] = function;
        }

        
    }
}