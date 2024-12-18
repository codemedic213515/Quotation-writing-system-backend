import { defineConfig } from "vitest/config"
import react from "@vitejs/plugin-react"
import { resolve, dirname } from "node:path";
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    open: true,
  },
  resolve: {
    alias: {
        '@': path.resolve(__dirname, './src'),
    },
  },
  base: "", /* this was set to "/" and I was having the wrong asset paths in the production index.html file (https://github.com/vitejs/vite/issues/4375) */
  test: {
    globals: true,
    environment: "jsdom",
    setupFiles: "src/setupTests",
    mockReset: true,
  },
  //build: { chunkSizeWarningLimit: 6400, }
})
