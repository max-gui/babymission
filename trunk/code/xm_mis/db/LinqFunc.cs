using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

namespace xm_mis.db
{
    static public class LinqFunc
    {
        static public DataTable ToDataTable<T>(this IEnumerable<T> varlist)//, CreateRowDelegate<T> fn)
        {

            DataTable dtReturn = new DataTable();

            // column names

            PropertyInfo[] oProps = null;

            // Could add a check to verify that there is an element 0

            foreach (T rec in varlist)
            {

                // Use reflection to get property names, to create table, Only first time, others will follow

                if (oProps == null)
                {

                    oProps = ((Type)rec.GetType()).GetProperties();

                    foreach (PropertyInfo pi in oProps)
                    {

                        Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {

                            colType = colType.GetGenericArguments()[0];

                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));

                    }

                }

                DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                {

                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);

                }

                dtReturn.Rows.Add(dr);

            }

            return (dtReturn);

        }
    }

    static public class AnonymousTypeTrick
    {
        static public T AnonymousCast<T>(this Object obj, T sample)//, CreateRowDelegate<T> fn)
        {

            return (T)obj;

        }
    }

    class MainIdComparer<T> : IEqualityComparer<T>
    {
        private string mainIdName;

        public MainIdComparer(string strMainIdName)
        {
            mainIdName = strMainIdName;
        }
        // Products are equal if their names and product numbers are equal.
        public bool Equals(T x, T y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            Type t = typeof(T);

            Int32 x_mainId = (Int32)t.InvokeMember(mainIdName, BindingFlags.GetProperty, null, x, null);
            Int32 y_mainId = (Int32)t.InvokeMember(mainIdName, BindingFlags.GetProperty, null, y, null);
            //Check whether the products' properties are equal.
            return x_mainId == y_mainId;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(T element)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(element, null)) return 0;

            //Get hash code for the Name field if it is not null.
            //int hashProductName = product.ProductPurposeResult == null ? 0 : product.ProductPurposeResult.GetHashCode();

            //Get hash code for the Code field.
            //int hashProductCode = product.ProductStockId.GetHashCode();

            //Calculate the hash code for the product.
            //return hashProductName ^ hashProductCode;
            Type t = typeof(T);

            Int32 element_mainId = (Int32)t.InvokeMember(mainIdName, BindingFlags.GetProperty, null, element, null);

            Int32 hashMainIdCode = element_mainId.GetHashCode();

            return hashMainIdCode;
        }
    }
}