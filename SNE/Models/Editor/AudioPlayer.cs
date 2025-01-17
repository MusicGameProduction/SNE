﻿using NAudio.Wave;
using System;
using System.ComponentModel;

namespace SNE.Models.Editor
{
    public class AudioPlayer : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Mp3FileReader AudioFileReader { get; set; }
        private WaveOut WaveOut { get; set; } = new WaveOut();

        public bool IsInitialized { get; set; } = false;
        public bool IsPlaying
        {
            get
            {
                if (this.WaveOut.PlaybackState != PlaybackState.Playing)
                    return false;

                else
                    return true;
            }
        }

        public double Volume
        {
            get
            {
                return this.WaveOut.Volume;
            }
            set
            {
                this.WaveOut.Volume = (float)value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Volume)));
            }
        }

        public double CurrentTimeSeconds
        {
            get
            {
                if (this.IsInitialized)
                    return this.AudioFileReader.CurrentTime.TotalSeconds;

                else
                    return 0;
            }
            set
            {
                if (this.IsInitialized)
                {
                    this.AudioFileReader.CurrentTime = TimeSpan.FromSeconds(value);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTimeSeconds)));
                }
            }
        }

        public double TotalTimeSeconds
        {
            get
            {
                if (this.IsInitialized)
                    return this.AudioFileReader.TotalTime.TotalSeconds;

                else
                    return 0;
            }
            set
            {
                // nothing do
            }
        }

        public string AudioFilePath { get; set; }

        public void Initialize(string mp3FileName)
        {
            if (this.IsInitialized)
                this.Dispose();

            this.AudioFileReader = new Mp3FileReader(mp3FileName);
            this.AudioFilePath = mp3FileName;
            this.WaveOut = new WaveOut();
            this.WaveOut.Init(this.AudioFileReader);
            this.Volume = 0.1;
            this.IsInitialized = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        public void Dispose()
        {
            this.AudioFileReader.Dispose();
            this.WaveOut.Dispose();
            this.IsInitialized = false;
        }

        public void Play()
        {
            if (this.IsInitialized)
                this.WaveOut.Play();
        }

        public void Pause()
        {
            if (this.IsInitialized)
                this.WaveOut.Pause();
        }

        public void Stop()
        {
            if (this.IsInitialized)
                this.WaveOut.Stop();

        }

        public void Back(TimeSpan time)
        {
            if (this.IsInitialized && this.CurrentTimeSeconds > time.TotalSeconds)
                this.CurrentTimeSeconds -= time.TotalSeconds;
        }

        public void Forward(TimeSpan time)
        {
            if (this.IsInitialized && this.TotalTimeSeconds - this.CurrentTimeSeconds > time.TotalSeconds)
                this.CurrentTimeSeconds += time.TotalSeconds;
        }
    }
}
