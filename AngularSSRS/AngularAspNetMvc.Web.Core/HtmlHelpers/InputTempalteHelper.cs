using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AngularAspNetMvc.Web.Core.Validation;

namespace AngularAspNetMvc.Web.Core.HtmlHelpers
{
    public static class InputTempalteHelper
    {

        public static CustomMvcForm BeginCustomForm(
            this HtmlHelper htmlHelper,
            string formName,
            IDictionary<string, object> htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", formName);
            tagBuilder.MergeAttribute("role", "form");
            tagBuilder.MergeAttribute("novalidate", "novalidate");
            tagBuilder.MergeAttribute("data-ng-cloak", "");
            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            var result = new CustomMvcForm(htmlHelper.ViewContext, formName);
            return result;
        }

        private static string GetInputTypeString(InputTagType inputType)
        {
            switch (inputType)
            {
                case InputTagType.CheckBox:
                    return "checkbox";
                case InputTagType.Select:
                    return "select";
                case InputTagType.Text:
                    return "text";
                default:
                    return "text";
            }
        }

        public static MvcHtmlString CustomSaveButton(
            this HtmlHelper htmlHelper,
            string saveMethodName = "save()",
            string saveLabel = "Save",
            string formName = null,
            IDictionary<string, object> htmlAttributes = null)
        {
            var builder = new TagBuilder("button");
            builder.AddCssClass("saveButton");
            builder.AddCssClass("btn-primary");
            builder.AddCssClass("btn");
            
            if (!saveMethodName.EndsWith("()"))
            {
                saveMethodName = saveMethodName + "()";
            }
            builder.MergeAttribute("data-form-submit-function", saveMethodName);
            builder.MergeAttribute("data-form-submit", 
                formName ?? htmlHelper.ViewContext.ViewData[CustomMvcForm.CurrentFormViewDataKey].ToString());
            builder.SetInnerText(saveLabel);
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal)); 
        }

        public static MvcHtmlString CustomCancelButton(
            this HtmlHelper htmlHelper,
            string cancelMethodName = "cancel()",
            string cancelLabel = "Cancel",
            IDictionary<string, object> htmlAttributes = null)
        {
            var builder = new TagBuilder("button");
            builder.AddCssClass("cancelButton");
            builder.AddCssClass("btn-warning");
            builder.AddCssClass("btn");
            if (!cancelMethodName.EndsWith("()"))
            {
                cancelMethodName = cancelMethodName + "()";
            }
            builder.MergeAttribute("data-ng-click", cancelMethodName);
            builder.SetInnerText(cancelLabel);
            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString CustomInputFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            InputTagType inputTagType = InputTagType.Text,
            string formName = null,
            IDictionary<string, object> htmlAttributes = null)
        {


            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var groupBuilder = CreateGroupBuilder(inputTagType);

            var labelBuilder = CreateLabelBuilder(modelMetadata);

            var name = ExpressionHelper.GetExpressionText(expression);

            TagBuilder inputBuilder = CreateInputTagBuilder(modelMetadata, name, inputTagType, htmlAttributes);

            var stringBuilder = new StringBuilder();
            if (inputTagType != InputTagType.CheckBox)
            {
                stringBuilder.AppendLine(labelBuilder.ToString(TagRenderMode.Normal));
                stringBuilder.AppendLine(inputBuilder.ToString(TagRenderMode.Normal));
            }
            else
            {
                labelBuilder.InnerHtml = inputBuilder.ToString(TagRenderMode.Normal) + labelBuilder.InnerHtml;
                stringBuilder.AppendLine(labelBuilder.ToString(TagRenderMode.Normal));
            }

            AddValidationErrorMessageDivs(formName ?? htmlHelper.ViewContext.ViewData[CustomMvcForm.CurrentFormViewDataKey].ToString(), modelMetadata, name, stringBuilder);

            groupBuilder.InnerHtml = stringBuilder.ToString();
            return new MvcHtmlString(groupBuilder.ToString(TagRenderMode.Normal));

        }


        private static void AddValidationErrorMessageDivs(
            string formName,
            ModelMetadata modelMetadata,
            string name,
            StringBuilder stringBuilder)
        {
            foreach (var additionalValue in modelMetadata.AdditionalValues.Values)
            {
                var attribute = additionalValue as ValidationAttributeMetadata;
                if (attribute != null)
                {
                    var builder = new TagBuilder("div");
                    builder.MergeAttribute("ng-show",
                        string.Format("showError({0}.{1}, '{2}')", formName, name, attribute.DirectiveName));
                    builder.AddCssClass("text-danger");
                    builder.SetInnerText(attribute.ErrorMessage);
                    stringBuilder.AppendLine(builder.ToString(TagRenderMode.Normal));
                }
            }
        }

        private static TagBuilder CreateGroupBuilder(InputTagType inputTagType)
        {
            var groupBuilder = new TagBuilder("div");
            groupBuilder.AddCssClass("form-group");
            if (inputTagType == InputTagType.CheckBox)
            {
                groupBuilder.AddCssClass("checkbox");
            }
            return groupBuilder;
        }

        private static TagBuilder CreateInputTagBuilder(
            ModelMetadata modelMetadata,
            string fullHtmlFieldName,
            InputTagType inputTagType,
            IDictionary<string, object> htmlAttributes)
        {
            TagBuilder inputBuilder;
            if (inputTagType == InputTagType.Select)
            {
                inputBuilder = new TagBuilder("select");
            }
            else
            {
                inputBuilder = new TagBuilder("input");
            }
            if (htmlAttributes != null)
            {
                inputBuilder.MergeAttributes(htmlAttributes);
            }
            inputBuilder.MergeAttribute("type", GetInputTypeString(inputTagType));
            inputBuilder.MergeAttribute("name", fullHtmlFieldName, true);
            if (inputTagType != InputTagType.CheckBox)
            {
                inputBuilder.AddCssClass("form-control");
            }
            inputBuilder.MergeAttribute("data-ng-model", string.Format("model.{0}", fullHtmlFieldName), true);

            foreach (var additionalValue in modelMetadata.AdditionalValues.Values)
            {
                var attribute = additionalValue as ValidationAttributeMetadata;
                if (attribute != null)
                {
                    inputBuilder.MergeAttribute(NormalizeDirectiveName(attribute.DirectiveName), attribute.AttributeValue, true);
                    if (attribute.AdditionalAttributes != null)
                    {
                        foreach (var key in attribute.AdditionalAttributes.Keys)
                        {
                            inputBuilder.MergeAttribute(key, attribute.AdditionalAttributes[key]);
                        }
                    }
                }

            }
            return inputBuilder;
        }

        private static string NormalizeDirectiveName(string directiveName)
        {
            var returnValue = "data";
            var values = Regex.Split(directiveName, @"(?<!^)(?=[A-Z])");
            foreach (var value in values)
            {
                returnValue = returnValue + "-" + value;
            }
            return returnValue;
        }

        private static TagBuilder CreateLabelBuilder(ModelMetadata modelMetadata)
        {
            var labelBuilder = new TagBuilder("label");
            labelBuilder.SetInnerText(modelMetadata.DisplayName ?? modelMetadata.PropertyName);
            return labelBuilder;
        }
    }
}
