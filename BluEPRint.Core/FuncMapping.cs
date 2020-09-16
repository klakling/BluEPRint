using System;

namespace BluEPRint.Core
{
    public static class FuncMapping
    {
       
        public static Func<TIn, TOut> Map<TIn, TOut, TOriginalIn>(
            Func<TOriginalIn, TOut> input, Func<TIn, TOriginalIn> convert) =>
            x => input(convert(x));

        /// <summary>
        /// Func Mapping Between doubles and float as Oxyplot wants double
        /// and ChemSharp provides float.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TOriginalIn"></typeparam>
        /// <param name="input"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static Func<double,double> Map(Func<float,float> original) => Map((float s) => (double)original(s), (double s) => (float)s);
    }
}
