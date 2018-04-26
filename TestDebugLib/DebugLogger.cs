using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TestDebugLib
{
    /// <summary>
    /// Локальный логгер для отладки.
    /// </summary>
    public class Logger
    {
        #region Public

        public static Logger GetLogger()
        {
            return logger;
        }

        /// <summary>
        /// Инкапсюляция процедуры логгирования.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }

        /// <summary>
        /// Добавляет объект для сравнения.
        /// </summary>
        /// <param name="obj">Объект для исследования.</param>
        /// <param name="field">Поле, по которому определяется, нужно ли исследовать
        /// объект.</param>
        /// <param name="pattern">Паттерн, которому должно соответсвовать значение
        /// в поле.</param>
        public void ComparedObject(object obj, string uniqField, string pattern)
        {
            if (Regex.IsMatch(uniqField, pattern))
            {
                var dictionary = new Dictionary<string, string>();
                var derived = obj.GetType();
                do
                {
                    var fields = derived.GetFields(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var field in fields)
                    {
                        string fieldVal = field.GetValue(obj) != null ?
                            field.GetValue(obj).ToString() : "null";
                        dictionary.Add(derived.FullName + ":" + field.Name, fieldVal);
                    }
                    derived = derived.BaseType;
                } while (derived != null);
                this.listObjects.Add(dictionary);
            }
        }

        /// <summary>
        /// Выводит лог сравнения объектов.
        /// </summary>
        public void CompareObjsLog()
        {
            if (this.listObjects.Count > 0)
            {
                List<string> fields = this.listObjects[0].Keys.ToList<string>();
                foreach (var field in fields)
                {
                    StringBuilder compareStr = new StringBuilder();
                    compareStr.AppendFormat("{0,-100}", field);
                    foreach (var dictionary in this.listObjects)
                    {
                        compareStr.AppendFormat("{0,-30}", dictionary[field]);
                    }
                    this.Log(compareStr.ToString());
                }
            }
        }

        /// <summary>
        /// Выводит лог сравнения разных полей объектов.
        /// </summary>
        public void CompareObjsDiffLog()
        {
            if (this.listObjects.Count > 1)
            {
                this.Log("Log.CompareObjsDiffLog(): Start.");
                List<string> fields = this.listObjects[0].Keys.ToList<string>();
                foreach (var field in fields)
                {
                    StringBuilder compareStr = new StringBuilder();
                    string oldVal = this.listObjects[0][field];
                    bool isDiff = false;
                    compareStr.AppendFormat("{0,-80}", field);
                    foreach (var dictionary in this.listObjects)
                    {
                        if (oldVal != dictionary[field])
                        {
                            isDiff = true;
                        }
                        compareStr.AppendFormat("{0,-60}", dictionary[field]);
                    }
                    if (isDiff)
                    {
                        this.Log(compareStr.ToString());
                    }
                }
                this.Log("Log.CompareObjsDiffLog(): Finish.");
            }
        }

        /// <summary>
        /// Лог, который выводит всю возможную информацию о классе.
        /// </summary>
        /// <param name="type"></param>
        /// <remarks>
        /// Для исследования лога можно использовать разные инструменты.
        /// </remarks>
        /// <example>
        /// Пример исследования лога с помощью PowerShell:
        /// ...
        /// $a = "path to log"
        /// Get-Content $a -Encoding UTF8 | Select-String -Pattern Der.*
        /// ...
        /// Пример извлечения из лога инфы по публичным и защищенным сущностям
        /// Get-Content $a -Encoding UTF8 | Select-String -Pattern Der.*[+#].* | Sort-Object -Descending -Unique
        /// ...
        /// 
        /// </example>
        public void LogAllAboutClass(Type type)
        {
            if (!this.callLogAllAboutClass)
            {
                List<Type> types = new List<Type>();
                types.Add(type);
                this.Log(string.Format("Derive: {0} связан с классом: ",
                    type.FullName));
                var derived = type;
                do
                {
                    derived = derived.BaseType;
                    if (derived != null)
                    {
                        types.Add(derived);
                        this.Log(string.Format("Derive:   {0}", derived.FullName));
                    }
                } while (derived != null);

                // Начинаем прогонять по полям, свойствам методам классов.
                types.Reverse();
                foreach (var t in types)
                {
                    this.Log(string.Format("Derive:   Сущность: {0}", t.FullName));
                    this.Log("Derive:       Поля:");
                    var fields = t.GetFields(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var field in fields)
                    {
                        string publisity = "";
                        if (field.IsPublic)
                        {
                            publisity = "+";
                        }
                        else if (field.IsPrivate)
                        {
                            publisity = "-";
                        }
                        else if (field.IsFamily)
                        {
                            publisity = "#";
                        }
                        else if (field.IsAssembly)
                        {
                            publisity = "~";
                        }
                        this.Log(string.Format("Derive:           {1}{0}",
                            field.Name, publisity));
                    }
                    this.Log("Derive:       Свойства:");
                    var props = t.GetProperties(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var prop in props)
                    {
                        this.Log(string.Format("Derive:           {0}", prop.Name));
                    }
                    this.Log("Derive:       События:");
                    var evs = t.GetEvents(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var ev in evs)
                    {
                        this.Log(string.Format("Derive:           {0}", ev.Name));
                    }
                    this.Log("Derive:       Конструкторы:");
                    var constructs = t.GetConstructors(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var construct in constructs)
                    {

                        string publisity = "";
                        if (construct.IsPublic)
                        {
                            publisity = "+";
                        }
                        else if (construct.IsPrivate)
                        {
                            publisity = "-";
                        }
                        else if (construct.IsFamily)
                        {
                            publisity = "#";
                        }
                        else if (construct.IsAssembly)
                        {
                            publisity = "~";
                        }
                        this.Log(string.Format("Derive:           {1}{0}",
              construct.Name, publisity));
                    }
                    this.Log("Derive:       Методы:");
                    var meths = t.GetMethods(
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);
                    foreach (var meth in meths)
                    {
                        string publisity = "";
                        if (meth.IsPublic)
                        {
                            publisity = "+";
                        }
                        else if (meth.IsPrivate)
                        {
                            publisity = "-";
                        }
                        else if (meth.IsFamily)
                        {
                            publisity = "#";
                        }
                        else if (meth.IsAssembly)
                        {
                            publisity = "~";
                        }
                        this.Log($"Derive:           {publisity}{meth.Name}");
                    }

                }
            }
            this.callLogAllAboutClass = true;
        }

        /// <summary>
        /// Логгирование описания объекта.
        /// </summary>
        /// <param name="obj"></param>
        public void ObjLog(object obj)
        {
            // Отмечаем начало описания.
            this.Log("Начало описания свойств объекта" + obj.GetHashCode().ToString());
            this.Log("============================================");
            // Производим описание свойств объекта.
            //var dictionary = new Dictionary<string, string>();
            var derived = obj.GetType();
            do
            {
                var fields = derived.GetFields(
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Public);
                foreach (var field in fields)
                {
                    string fieldVal = field.GetValue(obj) != null ?
                        field.GetValue(obj).ToString() : "null";
                    this.Log(derived.FullName + ":" + field.Name + " Val: " + fieldVal);
                    //dictionary.Add(derived.FullName + ":" + field.Name, fieldVal);
                }
                derived = derived.BaseType;
            } while (derived != null);
            // Отмечаем окончание описания. 
            this.Log("============================================");
            this.Log("Окончание описания свойств объекта" +
                obj.GetHashCode().ToString());
        }

        #endregion

        #region Private

        /// <summary>
        /// Переменная показывает, вызывалась ли процедура callLogAllAboutClass.
        /// </summary>
        bool callLogAllAboutClass;

        static Logger logger = new Logger();

        /// <summary>
        /// Список объектов для сравнения.
        /// </summary>
        List<Dictionary<string, string>> listObjects =
            new List<Dictionary<string, string>>();

        private Logger()
        {
            FileStream loggerStream = new FileStream("Debug.txt",
                FileMode.OpenOrCreate);
            Debug.Listeners.Add(new TextWriterTraceListener(loggerStream));
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Debug.Indent();
        }

        ~Logger()
        {
            Debug.Listeners.Clear();
        }

        #endregion

    }
}
