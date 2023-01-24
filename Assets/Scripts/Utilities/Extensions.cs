using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtensionMethods
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns new Vector3(myVector.x, 0f, myVector.z)
        /// </summary>
        public static Vector3 SqueezeTo2D(this Vector3 v3) => new Vector3(v3.x, 0f, v3.z);

        /// <summary>
        /// Returns new Vector3(myVector.x, 0f, myVector.y)
        /// </summary>
        public static Vector3 To3D(this Vector2 v2) => new Vector3(v2.x, 0f, v2.y);

        /// <summary>
        /// Returns the vector with a clamped magnitude between "min" and "max"
        /// </summary>
        public static Vector3 ClampLength(this Vector3 v, float min, float max) => v.magnitude > max ? v.normalized * max : (v.magnitude < min ? v.normalized * min : v);

        /// <summary>
        /// Returns new Vector2(myVector.x, myVector.y)
        /// </summary>
        public static Vector2 ToUIVector(this Vector3 v) => new Vector2(v.x, v.y);

        /// <summary>
        /// Returns new Vector3(myVector.x, myVector.y, 0f)
        /// </summary>
        public static Vector3 UIToWorldVector(this Vector2 v) => new Vector3(v.x, v.y, 0f);

        /// <summary>
        /// Returns new Vector3(method(oldx), method(oldy), method(oldz))
        /// </summary>
        public static Vector3 ApplyMethodToComponents(this Vector3 v, Func<float, float> method) => new Vector3(method.Invoke(v.x), method.Invoke(v.y), method.Invoke(v.z));

        /// <summary>
        /// Returns new Vector3(method(oldx), method(oldy), method(oldz))
        /// </summary>
        public static Vector2 ApplyMethodToComponents(this Vector2 v, Func<float, float> method) => new Vector2(method.Invoke(v.x), method.Invoke(v.y));

        /// <summary>
        /// Returns the vector multiplied by rotation
        /// </summary>
        public static Vector3 Rotate(this Vector3 v, float x, float y, float z)
        {
            Quaternion rotation = Quaternion.Euler(x, y, z);
            return rotation * v;
        }

        /// <summary>
        /// Return the sum of all 3D vectors in enumerable.
        /// </summary>
        public static Vector3 SumOfVectors3(this IEnumerable<Vector3> enumerable)
        {
            var sum = Vector3.zero;
            foreach (var x in enumerable) sum += x;
            return sum;
        }

        /// <summary>
        /// Return the sum of all 2D vectors in enumerable.
        /// </summary>
        public static Vector2 SumOfVectors2(this IEnumerable<Vector2> enumerable)
        {
            var sum = Vector2.zero;
            foreach (var x in enumerable) sum += x;
            return sum;
        }
    }

    public static class TransformExtensions
    {
        /// <summary>
        /// Rotates transform around point bv "angle" degrees on the y axis
        /// </summary>
        public static void RotateAroundPointY(this Transform transform, Vector3 point, float angle)
        {
            transform.Rotate(0f, angle, 0f);
            Vector3 v = point - transform.position;
            transform.position = point - v.Rotate(0f, angle, 0f);
        }

        /// <summary>
        /// Rotates transform on the Y axis only, soi that transform.forward (blue axis) points towards the direction of position in World Space
        /// </summary>
        public static void LookAtHorizontally(this Transform t, Vector3 position) => t.LookAt(t.position + position.SqueezeTo2D() - t.position.SqueezeTo2D(), Vector3.up);
        /// <summary>
        /// Rotates transform on the Y axis only, soi that transform.forward (blue axis) points towards the direction of position in World Space
        /// </summary>
        public static void LookAtHorizontally(this Transform t, Transform position) => t.LookAt(t.position + position.position.SqueezeTo2D() - t.position.SqueezeTo2D(), Vector3.up);


        /// <summary>
        /// Slerps from current rotation to look at rotation for position, separately on horizontal and vertical axis.
        /// </summary>
        public static void LookAtLerped(this Transform t, Vector3 position, float horizontalPercentage, float verticalPercentage)
        {
            t.LookAt(t.position + Vector3.Slerp(t.forward, position.SqueezeTo2D() - t.position.SqueezeTo2D(), horizontalPercentage), Vector3.up);
            Vector3 eulers = t.rotation.eulerAngles;
            t.LookAt(t.position + Vector3.Slerp(t.forward, (position - t.position).normalized, verticalPercentage), Vector3.up);
            t.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, eulers.y, eulers.z);
        }

        /// <summary>
        /// Sets all children layers of this Transform and its own to specified layer (from index).
        /// </summary>
        public static void SetLayerRecursively(this Transform t, int layerIndex)
        {
            var go = t.gameObject;
            go.layer = layerIndex;
            foreach (Transform t2 in go.transform)
            {
                SetLayerRecursively(t2, layerIndex);
            }
        }
    }

    public static class GameObjectExtensions
    {
        /// <summary>
        /// Sets all children layers of this GameObject and its own to specified layer (from index).
        /// </summary>
        public static void SetLayerRecursively(this GameObject go, int layerIndex)
        {
            go.layer = layerIndex;
            foreach (Transform t in go.transform)
            {
                SetLayerRecursively(t.gameObject, layerIndex);
            }
        }
    }

    public static class UIExtensions
    {
        /// <summary>
        /// Sets the UI Anchors of this Rect Transform to its corners. (Works only if rotation is 0)
        /// </summary>
        public static void SetAnchorsToCorners(this RectTransform t)
        {
            if (t == null) return;
            RectTransform pt = (RectTransform)t.parent;

            if (pt == null)
            {
                t.anchorMin = Vector2.zero;
                t.anchorMax = Vector2.one;
                return;
            }


            Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                             t.anchorMin.y + t.offsetMin.y / pt.rect.height);
            Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                             t.anchorMax.y + t.offsetMax.y / pt.rect.height);

            t.anchorMin = newAnchorsMin;
            t.anchorMax = newAnchorsMax;
            t.offsetMin = t.offsetMax = new Vector2(0, 0);
        }

        /// <summary>
        /// Sets the UI Anchors of this Rect Transform to the corners of the parent Rect Transform.
        /// </summary>
        public static void SetAnchorsToParentCorners(this RectTransform t)
        {
            t.anchorMin = Vector2.zero;
            t.anchorMax = Vector2.one;
        }

        /// <summary>
        /// Sets the distance from the LEFT side of the box bounded by the anchors.
        /// </summary>
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }
        /// <summary>
        /// Sets the distance from the RIGHT side of the box bounded by the anchors.
        /// </summary>
        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }
        /// <summary>
        /// Sets the distance from the TOP side of the box bounded by the anchors.
        /// </summary>
        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }
        /// <summary>
        /// Sets the distance from the BOTTOM side of the box bounded by the anchors.
        /// </summary>
        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
        /// <summary>
        /// Preserves the Texture's Aspect Ration on a Raw Image.
        /// </summary>
        public static void SetRawImagePreserveAspect(this RawImage image)
        {
            var tex = image.texture;
            var t = image.GetComponent<RectTransform>();
            bool isWidthMax = tex.width > tex.height;
            Rect rect = new Rect(t.rect.position, new Vector2(t.rect.width - t.sizeDelta.x, t.rect.height - t.sizeDelta.y));
            if (isWidthMax)
            {
                float topdown = (rect.height - rect.height * (tex.height / (float)tex.width)) / 2f;
                t.SetTop(topdown);
                t.SetBottom(topdown);
                t.SetLeft(0f);
                t.SetRight(0f);
            }
            else
            {
                float leftRight = (rect.width - rect.width * (tex.width / (float)tex.height)) / 2f;
                t.SetLeft(leftRight);
                t.SetRight(leftRight);
                t.SetTop(0f);
                t.SetBottom(0f);
            }
        }
        /// <summary>
        /// Returns a Texture2D which is a cropped version of the original RawImage's texture, inside the overlay Rect Transform.
        /// </summary>
        public static Texture2D CropImageWithOverlay(this RawImage image, RectTransform overlay)
        {
            Texture mainTexture = image.texture;
            Texture2D tex = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);

            RenderTexture currentRT = RenderTexture.active;

            RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
            Graphics.Blit(mainTexture, renderTexture);

            RenderTexture.active = renderTexture;
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex.Apply();
            RenderTexture.active = currentRT;

            RectTransform rectImage = image.GetComponent<RectTransform>();

            Vector3[] cornersImage = new Vector3[4];
            rectImage.GetWorldCorners(cornersImage);
            Rect imageRect = new Rect(new Vector2(0, 0), new Vector2(Mathf.Abs(cornersImage[0].x - cornersImage[3].x), Mathf.Abs(cornersImage[0].y - cornersImage[1].y)));

            Vector3[] cornersOver = new Vector3[4];
            overlay.GetWorldCorners(cornersOver);
            Rect overlayRect = new Rect(new Vector2(0, 0), new Vector2(Mathf.Abs(cornersOver[0].x - cornersOver[3].x), Mathf.Abs(cornersOver[0].y - cornersOver[1].y)));

            float widthRatio = overlayRect.width / imageRect.width;
            float heightRatio = overlayRect.height / imageRect.height;

            int cropSizeX = (int)(widthRatio * tex.width);
            int cropSizeY = (int)(heightRatio * tex.height);

            Color[] ogCropped = tex.GetPixels((tex.width - cropSizeX) / 2, (tex.height - cropSizeY) / 2, cropSizeX, cropSizeY);
            Texture2D rtrn = new Texture2D(cropSizeX, cropSizeY);
            rtrn.SetPixels(ogCropped);
            rtrn.Apply();
            return rtrn;
        }

        /// <summary>
        /// Returns the calculated Rect of this transform in world space.
        /// </summary>
        public static Rect GetWorldRect(this RectTransform t)
        {
            Vector3[] corners = new Vector3[4];
            t.GetWorldCorners(corners);
            return new Rect(t.position, new Vector2(Mathf.Abs(corners[0].x - corners[3].x), Mathf.Abs(corners[0].y - corners[1].y)));
        }
    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs func on every value type element of IList.
        /// </summary>
        public static void PerformFuncOnValueElements<T>(this IList<T> list, Func<T, T> func) where T : struct
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = func.Invoke(list[i]);
            }
        }
        /// <summary>
        /// Performs func on every value type element of IEnumerable and returns its new version.
        /// </summary>
        public static OUT PerformFuncOnValueElements<T, OUT>(this IEnumerable<T> enumerable, Func<T, T> func) where T : struct where OUT : IEnumerable
        {
            return (OUT)(from x in enumerable select func.Invoke(x));
        }
        /// <summary>
        /// Performs func on every reference type element of IList.
        /// </summary>
        public static void PerformFuncOnReferenceElements<T>(this IEnumerable<T> enumerable, Action<T> func) where T : class
        {
            for (int i = 0; i < enumerable.Count(); i++)
            {
                func.Invoke(enumerable.ElementAt(i));
            }
        }

        /// <summary>
        /// Searches an enumerable for matches with specified query. Finds matches of Regex($".*{query}.*")
        /// </summary>
        /// <param name="enumerable">The enumerable to perform the search query on.</param>
        /// <param name="query">The value to search for.</param>
        /// <param name="selector">What to select for each element in the enumerable, to be used in the search</param>
        /// <returns></returns>
        public static IEnumerable<T> SortBySearch<T>(this IEnumerable<T> enumerable, string query, Func<T, string> selector)
        {
            var regex = new Regex($".*{query}.*", RegexOptions.IgnoreCase);
            return from x in enumerable where regex.IsMatch(selector.Invoke(x)) select x;
        }
    }
}
