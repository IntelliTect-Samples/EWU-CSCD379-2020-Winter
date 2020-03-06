﻿// lib
const autoprefixer = require('autoprefixer');
const path = require('path');
const webpack = require('webpack');

// plugins
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = (env, argv) => {
    const isProd = argv.mode === 'production';

    // paths
    const distPath = path.resolve(__dirname, './wwwroot');
    const imgDistPath = path.resolve(distPath, './Assets/images');
    const iconDistPath = path.resolve(distPath, './Assets/icon');
    const srcPath = '.';
    const templatePath = path.resolve(__dirname, './Views/Shared');

    return {
        entry: {
            main: './scripts/main.ts'
        },
        output: {
            filename: 'js/[name].[hash].js',
            path: distPath,
            publicPath: '/'
        },

        resolve: {
            extensions: ['.ts', '.js', '.json', '.tsx', '.jsx', '.vue'],
            alias: {
                'vue$': 'vue/dist/vue.esm.js'
            },
            modules: [
                path.resolve(__dirname, './node_modules'),
                srcPath
            ]
        },

        module: {
            rules: [
                {
                    test: /\.scss$/,
                    use: [
                        { loader: MiniCssExtractPlugin.loader },
                        { loader: 'css-loader' },
                        { loader: 'postcss-loader', options: { ident: 'postcss', plugins: () => [autoprefixer()] } },
                        { loader: 'sass-loader', options: { implementation: require('sass'), sassOptions: { fiber: require('fibers') } } }
                    ]
                },
                {
                    test: /\.(png|jpg|gif|svg|ico$)$/,
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]?[hash]'
                    }
                },
                {
                    test: /\.tsx?$/,
                    loader: 'ts-loader',
                    exclude: /node_modules/,
                    options: {
                        appendTsSuffixTo: [/\.vue$/]
                    }
                },
                {
                    test: /\.vue$/,
                    loader: 'vue-loader',
                    options: {
                        loaders: {
                            'scss': 'vue-style-loader!css-loader!sass-loader',
                            'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                        }
                    }
                }
            ]
        },
        plugins: [
            new CleanWebpackPlugin(),
            new VueLoaderPlugin(),

            // copy src images to wwwroot
            new CopyWebpackPlugin(
                [
                     {from: 'favicon', to: distPath},
                     {from: 'icons', to: iconDistPath},
                    //{ from: 'images', to: imgDistPath }
                ],
                {
                    context: srcPath,
                    ignore: ['*.DS_Store']
                }),

            new MiniCssExtractPlugin({
                filename: 'css/[name].[hash].css',
                path: distPath,
                publicPath: '/'
            }),

            new HtmlWebpackPlugin({
                filename: path.resolve(templatePath, './_Layout.cshtml'),
                inject: false,
                minify: false,
                template: path.resolve(templatePath, './_Layout_Template.cshtml')
            })
        ],
        devtool: '#eval-source-map'
    };
};