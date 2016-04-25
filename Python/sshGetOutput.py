# SSH Capturing Output Test Program
# Implemented by William M Mortl
# Coded for Python 2.7.9
# python sshGetOutput.py will numa1.cs.colorado.edu /

# imports
from subprocess import check_output
from subprocess import PIPE
from subprocess import Popen
import re
import sys

# constants
CMD_SSH_1 = "ssh"
CMD_SSH_2 = "%s@%s"
CMD_SSH_3 = "-t"
CMD_LS_1 = "ls"
CMD_LS_2 = "-la"

# ssh and run a command, assumes ssh key is on the server
def cmdSSHRunCommand(userName, host, command):
	ssh = Popen([CMD_SSH_1, CMD_SSH_2 % (userName, host), CMD_SSH_3, command], shell = False, stdout = PIPE, stderr = PIPE)
	return ssh.stdout.readlines()

# query and get values for the UUID, returns key-value dictionary
def localLS(directory):
	outputText = check_output([CMD_LS_1, CMD_LS_2, directory])
	return outputText

# generates a remote file
def sshLS(host, username, directory):
	lsCommand = CMD_LS_1 + " " + CMD_LS_2 + " " + directory
	return cmdSSHRunCommand(host, username, lsCommand)

# main entry point
if __name__ == "__main__":
	if (len(sys.argv) < 4):
		print("\r\nFluidMem Memory Attachment by William M Mortl")
		print("Usage: python sshGetOutput.py {username} {server host} {directory}")
		print("Example: python sshGetOutput.py will numa1.cs.colorado.edu /\r\n")
	else:

		# extract command line parameters
		userName = sys.argv[1]
		host = sys.argv[2]
		directory = sys.argv[3]

		# get local directory
		print("\r\nLocal directory...\r\n")
		print(localLS(directory))

		# get ssh directory
		print("\r\nSSH directory...\r\n")
		print(sshLS(userName, host, directory))