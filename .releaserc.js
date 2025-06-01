const { analyzeCommits } = require('@semantic-release/commit-analyzer');

module.exports = {
  branches: ['main'],
  plugins: [
    '@semantic-release/commit-analyzer',
    '@semantic-release/release-notes-generator',
    [
      '@semantic-release/changelog',
      {
        changelogFile: 'CHANGELOG.md'
      }
    ],
    [
      '@semantic-release/git',
      {
        assets: ['CHANGELOG.md', 'README.md'],
        message: 'chore(release): ${nextRelease.version} [skip ci]\n\n${nextRelease.notes}'
      }
    ],
    '@semantic-release/github'
  ],
  analyzeCommits: async (pluginConfig, context) => {
    const result = await analyzeCommits(pluginConfig, context);
    // Se não houver releaseType detectado, assume patch
    return result || 'patch';
  }
};
