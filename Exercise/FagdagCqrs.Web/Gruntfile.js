/* global module, require */

module.exports = function (grunt) {
    require('load-grunt-tasks')(grunt);

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        jshint: {
            options: {
                jshintrc: true
            },
            dev: {
                src: ['Gruntfile.js', 'App/**/*.js']
            }
        },
        
        concat: {
            dev: {
                options: {
                    sourceMap: true
                },
                src: ['App/index.js', 'App/**/*.js'],
                dest: 'Content/Scripts/built.js'
            }
        },

        bower: {
            options: {
                copy: false
            },
            install: {
            }
        },
        
        injector: {
            options: {
                relative: true,
                addRootSlash: false,
                lineEnding: '\r\n',
                bowerPrefix: 'bower:'
            },
            dev: {
                files: {
                    'index.html': ['App/index.js', 'App/**/*.js', 'bower.json']
                }
            }
        },
        watch: {
            js: {
                options: {
                    livereload: 35729
                },
                files: ['app/**/*.js', 'app/**/*.html', 'index.html'],
                tasks: ['injector:dev']
            }
        },
        connect: {
            options: {
                port: 61476,
                base: '.',
                livereload: 35729
            },
            dev: {
                options: {
                    open: {
                        target: 'http://localhost:61476'//,
                        //appName: 'open'
                    }
                }
            }
        }
    });

    grunt.registerTask('default', ['dev', 'watch']);
    grunt.registerTask('dev', ['bower', 'injector:dev']);
    grunt.registerTask('server', ['bower', 'injector:dev', 'connect', 'watch']);
    grunt.registerTask('build', ['bower', 'injector:dev']);
};