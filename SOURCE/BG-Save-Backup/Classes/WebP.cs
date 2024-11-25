using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace BG3_Save_Backup.Classes {
    internal class WebP {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPIDecoder { }
        public enum WEBP_CSP_MODE {
            MODE_RGB = 0,
            MODE_RGBA = 1,
            MODE_BGR = 2,
            MODE_BGRA = 3,
            MODE_ARGB = 4,
            MODE_RGBA_4444 = 5,
            MODE_RGB_565 = 6,
            MODE_rgbA = 7,
            MODE_bgrA = 8,
            MODE_Argb = 9,
            MODE_rgbA_4444 = 10,
            MODE_YUV = 11,
            MODE_YUVA = 12,
            MODE_LAST = 13
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPRGBABuffer {
            public IntPtr rgba;
            public int stride;
            public UIntPtr size;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPYUVABuffer {
            public IntPtr y;
            public IntPtr u;
            public IntPtr v;
            public IntPtr a;
            public int y_stride;
            public int u_stride;
            public int v_stride;
            public int a_stride;
            public UIntPtr y_size;
            public UIntPtr u_size;
            public UIntPtr v_size;
            public UIntPtr a_size;
        }
        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct Anonymous_690ed5ec_4c3d_40c6_9bd0_0747b5a28b54 {
            [FieldOffsetAttribute(0)]
            public WebPRGBABuffer RGBA;
            [FieldOffsetAttribute(0)]
            public WebPYUVABuffer YUVA;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPDecBuffer {
            public WEBP_CSP_MODE colorspace;
            public int width;
            public int height;
            public int is_external_memory;
            public Anonymous_690ed5ec_4c3d_40c6_9bd0_0747b5a28b54 u;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] pad;
            public IntPtr private_memory;
        }
        public enum VP8StatusCode {
            VP8_STATUS_OK = 0,
            VP8_STATUS_OUT_OF_MEMORY,
            VP8_STATUS_INVALID_PARAM,
            VP8_STATUS_BITSTREAM_ERROR,
            VP8_STATUS_UNSUPPORTED_FEATURE,
            VP8_STATUS_SUSPENDED,
            VP8_STATUS_USER_ABORT,
            VP8_STATUS_NOT_ENOUGH_DATA,
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPBitstreamFeatures {
            public int width;
            public int height;
            public int has_alpha;
            public int has_animation;
            public int format;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
            public uint[] pad;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPDecoderOptions {
            public int bypass_filtering;
            public int no_fancy_upsampling;
            public int use_cropping;
            public int crop_left, crop_top;

            public int crop_width, crop_height;
            public int use_scaling;
            public int scaled_width, scaled_height;
            public int use_threads;
            public int dithering_strength;

            public int flip;
            public int alpha_dithering_strength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.U4)]
            public uint[] pad;
        };
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct WebPDecoderConfig {
            public WebPBitstreamFeatures input;
            public WebPDecBuffer output;
            public WebPDecoderOptions options;
        }

        [DllImport("libwebp", EntryPoint = "WebPGetDecoderVersion", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetDecoderVersion();

        [DllImport("libwebp", EntryPoint = "WebPGetInfo", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPGetInfo([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeRGBA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeRGBA([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeARGB", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeARGB([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeBGRA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeBGRA([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeRGB", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeRGB([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeBGR", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeBGR([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPDecodeYUV", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeYUV([InAttribute()] IntPtr data, UIntPtr data_size, ref int width, ref int height, ref IntPtr u, ref IntPtr v, ref int stride, ref int uv_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeRGBAInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeRGBAInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeARGBInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeARGBInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeBGRAInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeBGRAInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeRGBInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeRGBInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeBGRInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeBGRInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPDecodeYUVInto", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPDecodeYUVInto([InAttribute()] IntPtr data, UIntPtr data_size, IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);

        [DllImport("libwebp", EntryPoint = "WebPInitDecBufferInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPInitDecBufferInternal(ref WebPDecBuffer param0, int param1);

        [DllImport("libwebp", EntryPoint = "WebPFreeDecBuffer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebPFreeDecBuffer(ref WebPDecBuffer buffer);

        [DllImport("libwebp", EntryPoint = "WebPINewDecoder", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPINewDecoder(ref WebPDecBuffer output_buffer);

        [DllImport("libwebp", EntryPoint = "WebPINewRGB", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPINewRGB(WEBP_CSP_MODE csp, IntPtr output_buffer, UIntPtr output_buffer_size, int output_stride);

        [DllImport("libwebp", EntryPoint = "WebPINewYUVA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPINewYUVA(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride, IntPtr a, UIntPtr a_size, int a_stride);

        [DllImport("libwebp", EntryPoint = "WebPINewYUV", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPINewYUV(IntPtr luma, UIntPtr luma_size, int luma_stride, IntPtr u, UIntPtr u_size, int u_stride, IntPtr v, UIntPtr v_size, int v_stride);

        [DllImport("libwebp", EntryPoint = "WebPIDelete", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WebPIDelete(ref WebPIDecoder idec);

        [DllImport("libwebp", EntryPoint = "WebPIAppend", CallingConvention = CallingConvention.Cdecl)]
        public static extern VP8StatusCode WebPIAppend(ref WebPIDecoder idec, [InAttribute()] IntPtr data, UIntPtr data_size);

        [DllImport("libwebp", EntryPoint = "WebPIUpdate", CallingConvention = CallingConvention.Cdecl)]
        public static extern VP8StatusCode WebPIUpdate(ref WebPIDecoder idec, [InAttribute()] IntPtr data, UIntPtr data_size);

        [DllImport("libwebp", EntryPoint = "WebPIDecGetRGB", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPIDecGetRGB(ref WebPIDecoder idec, ref int last_y, ref int width, ref int height, ref int stride);

        [DllImport("libwebp", EntryPoint = "WebPIDecGetYUVA", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPIDecGetYUVA(ref WebPIDecoder idec, ref int last_y, ref IntPtr u, ref IntPtr v, ref IntPtr a, ref int width, ref int height, ref int stride, ref int uv_stride, ref int a_stride);

        [DllImport("libwebp", EntryPoint = "WebPIDecodedArea", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPIDecodedArea(ref WebPIDecoder idec, ref int left, ref int top, ref int width, ref int height);

        [DllImport("libwebp", EntryPoint = "WebPGetFeaturesInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern VP8StatusCode WebPGetFeaturesInternal([InAttribute()] IntPtr param0, UIntPtr param1, ref WebPBitstreamFeatures param2, int param3);

        [DllImport("libwebp", EntryPoint = "WebPInitDecoderConfigInternal", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WebPInitDecoderConfigInternal(ref WebPDecoderConfig param0, int param1);

        [DllImport("libwebp", EntryPoint = "WebPIDecode", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr WebPIDecode([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        [DllImport("libwebp", EntryPoint = "WebPDecode", CallingConvention = CallingConvention.Cdecl)]
        public static extern VP8StatusCode WebPDecode([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        public const int WEBP_DECODER_ABI_VERSION = 520;
        public const int WEBP_ENCODER_ABI_VERSION = 521;
        public const int WEBP_MAX_DIMENSION = 16383;
        public static bool WebPIsPremultipliedMode(WEBP_CSP_MODE mode) {
            return (mode == WEBP_CSP_MODE.MODE_rgbA || mode == WEBP_CSP_MODE.MODE_bgrA || mode == WEBP_CSP_MODE.MODE_Argb ||
                mode == WEBP_CSP_MODE.MODE_rgbA_4444);
        }
        public static bool WebPIsRGBMode(WEBP_CSP_MODE mode) {
            return (mode < WEBP_CSP_MODE.MODE_YUV);
        }
        public static bool WebPIsAlphaMode(WEBP_CSP_MODE mode) {
            return (mode == WEBP_CSP_MODE.MODE_RGBA || mode == WEBP_CSP_MODE.MODE_BGRA || mode == WEBP_CSP_MODE.MODE_ARGB ||
                    mode == WEBP_CSP_MODE.MODE_RGBA_4444 || mode == WEBP_CSP_MODE.MODE_YUVA ||
                    WebPIsPremultipliedMode(mode));
        }
        public static VP8StatusCode WebPGetFeatures(IntPtr data, UIntPtr data_size, ref WebPBitstreamFeatures features) {
            return WebPGetFeaturesInternal(data, data_size, ref features, WEBP_DECODER_ABI_VERSION);
        }
        public static int WebPInitDecoderConfig(ref WebPDecoderConfig config) {
            return WebPInitDecoderConfigInternal(ref config, WEBP_DECODER_ABI_VERSION);
        }
        public static int WebPInitDecBuffer(ref WebPDecBuffer buffer) {
            return WebPInitDecBufferInternal(ref buffer, WEBP_DECODER_ABI_VERSION);
        }
        public static unsafe Bitmap DecodeFromBytes(byte[] data, long length) {
            fixed (byte* dataptr = data) {
                return DecodeFromPointer((IntPtr)dataptr, length);
            }
        }
        public static Bitmap DecodeFromPointer(IntPtr data, long length) {
            int w = 0, h = 0;
            //Validate header and determine size
            if (WebPGetInfo(data, (UIntPtr)length, ref w, ref h) == 0) throw new Exception("Invalid WebP header detected");

            bool success = false;
            Bitmap b = null;
            BitmapData bd = null;
            try {
                //Allocate canvas
                b = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //Lock surface for writing
                bd = b.LockBits(new Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                //Decode to surface
                IntPtr result = WebPDecodeBGRAInto(data, (UIntPtr)length, bd.Scan0, (UIntPtr)(bd.Stride * bd.Height), bd.Stride);
                if (bd.Scan0 != result) throw new Exception("Failed to decode WebP image with error " + (long)result);
                success = true;
            } finally {
                //Unlock surface
                if (bd != null && b != null) b.UnlockBits(bd);
                //Dispose of bitmap if anything went wrong
                if (!success && b != null) b.Dispose();
            }
            return b;
        }
    }
}

