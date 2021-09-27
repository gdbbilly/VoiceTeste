using System;
using Honeywell.AIDC.CrossPlatform;
using Android.Content;
using System.Collections.Generic;
using System.Linq;

namespace Voice.Picking.Test
{
    public class BarcodeReaderLaser
    {
        private const string DEFAULT_READER_KEY = "default";
        private Dictionary<string, BarcodeReader> mBarcodeReaders;
        private static BarcodeReader mSelectedReader = null;
        private static string mReaderPicker;

        public BarcodeReaderLaser()
        {
            mBarcodeReaders = new Dictionary<string, BarcodeReader>();
        }

        public async void PopulateReaderPicker(Context context)
        {
            try
            {
                // Queries the list of readers that are connected to the mobile computer.
                IList<BarcodeReaderInfo> readerList = await BarcodeReader.GetConnectedBarcodeReaders(context);

                if (readerList.Count > 0)
                {
                    mReaderPicker = readerList.FirstOrDefault().ScannerName;

                    OpenBarcodeReader(context);
                }
                else
                {
                    mReaderPicker = DEFAULT_READER_KEY;
                }
            }
            catch (Exception ex)
            {
                //mReaderPicker.Items.Add(DEFAULT_READER_KEY);
                //await DisplayAlert("Error", "Failed to query connected readers, " + ex.Message, "OK");
            }
            //mReaderPicker.SelectedIndex = 0;
        }

        public BarcodeReader GetBarcodeReader(Context context)
        {
            string readerName = mReaderPicker;
            BarcodeReader reader = null;

            if (readerName == DEFAULT_READER_KEY)
            { // This name was added to the Open Reader picker list if the
              // query for connected barcode readers failed. It is not a
              // valid reader name. Set the readerName to null to default
              // to internal scanner.
                readerName = null;
            }

            if (null == readerName)
            {
                if (mBarcodeReaders.ContainsKey(DEFAULT_READER_KEY))
                {
                    reader = mBarcodeReaders[DEFAULT_READER_KEY];
                }
            }
            else
            {
                if (mBarcodeReaders.ContainsKey(readerName))
                {
                    reader = mBarcodeReaders[readerName];
                }
            }

            if (null == reader)
            {
                // Create a new instance of BarcodeReader object.
                // Create an instance of BarcodeReader.
                reader = new BarcodeReader(readerName, context);

                // Add an event handler to receive barcode data.
                // Even though we may have multiple reader sessions, we only
                // have one event handler. In this app, no matter which reader
                // the data come frome it will update the same UI controls.
                //reader.BarcodeDataReady += MBarcodeReader_BarcodeDataReady;

                // Add the BarcodeReader object to mBarcodeReaders collection.
                if (null == readerName)
                {
                    mBarcodeReaders.Add(DEFAULT_READER_KEY, reader);
                }
                else
                {
                    mBarcodeReaders.Add(readerName, reader);
                }
            }

            return reader;
        }

        public string GetBarcodeName()
        {
            return mReaderPicker;
        }

        private void MBarcodeReader_BarcodeDataReady(object sender, BarcodeDataArgs e)
        {

        }

        public async void OpenBarcodeReader(Context context)
        {
            mSelectedReader = GetBarcodeReader(context);
            if (!mSelectedReader.IsReaderOpened)
            {
                BarcodeReader.Result result = await mSelectedReader.OpenAsync();

                if (result.Code == BarcodeReader.Result.Codes.SUCCESS ||
                    result.Code == BarcodeReader.Result.Codes.READER_ALREADY_OPENED)
                {
                    //Enable selected symbologyes
                    EnableSymbolgy();
                }
                else
                {
                    //await DisplayAlert("Error", "OpenAsync failed, Code:" + result.Code +
                    //    " Message:" + result.Message, "OK");
                }
            }
        }

        public async void CloseBarcodeReader()
        {
            if (mSelectedReader != null && mSelectedReader.IsReaderOpened)
            {
                BarcodeReader.Result result = await mSelectedReader.CloseAsync();
                if (result.Code == BarcodeReader.Result.Codes.SUCCESS)
                {

                }
                else
                {

                }
            }
        }

        private async void EnableSymbolgy()
        {
            try
            {
                if (mSelectedReader.IsReaderOpened)
                {
                    Dictionary<string, object> settings = new Dictionary<string, object>()
                    {
                        {mSelectedReader.SettingKeys.Code128Enabled, true },
                        {mSelectedReader.SettingKeys.Code93Enabled, true },
                        {mSelectedReader.SettingKeys.Code39Enabled, true },
                        {mSelectedReader.SettingKeys.Ean8Enabled, true },
                        {mSelectedReader.SettingKeys.Ean8CheckDigitTransmitEnabled, true },
                        {mSelectedReader.SettingKeys.Ean13Enabled, true },
                        {mSelectedReader.SettingKeys.Ean13CheckDigitTransmitEnabled, true },
                        {mSelectedReader.SettingKeys.Interleaved25Enabled, true },
                        {mSelectedReader.SettingKeys.Interleaved25MaximumLength, 100 },
                        {mSelectedReader.SettingKeys.Postal2DMode, mSelectedReader.SettingValues.Postal2DMode_Usps }
                    };

                    BarcodeReader.Result result = await mSelectedReader.SetAsync(settings);
                    if (result.Code != BarcodeReader.Result.Codes.SUCCESS)
                    {

                    }
                }
            }
            catch (Exception exp)
            {
                //await DisplayAlert("Error", "Symbology settings failed. Message: " + exp.Message, "OK");
            }
        }

    }
}