﻿
using System.IO.Compression;
using MusicPlayer.API;

var musicPlayer = new MusicPlayer.API.MusicPlayer();
musicPlayer.LoadPlaylist(new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"));
musicPlayer.Play(musicPlayer.Playlist.Songs[0]);

Console.ReadKey();