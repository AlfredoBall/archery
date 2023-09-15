/** @type {import('next').NextConfig} */
const nextConfig = {
    async redirects() {
        return [
            {
                source: '/',
                destination: '/settings',
                permanent: true
            }
        ]
    },
    // async rewrites() {
    //     return {
    //         beforeFiles: [,
    //             {
    //                 source: '/',
    //                 destination: '/settings'
    //             }
    //         ]
    //     }
    // }
}

module.exports = nextConfig