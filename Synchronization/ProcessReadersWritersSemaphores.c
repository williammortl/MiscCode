// This is the starvation-free solution to 2nd R/W on slide 26 of the synchronization
// 	problems powerpoint

/*
 *
 * Includes
 *
 */
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <pthread.h>
#include <semaphore.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>



/*
 *
 * Definitions
 *
 */
#define LEN_FILE 100
#define TRUE 1
#define FALSE 0
#define TOTAL_CHILDREN 6
#define SEM_WRITE "wmm sem write"
#define SEM_READ "wmm sem read"
#define SEM_MUTEX "wmm sem mutex"



/*
 *
 * Global variables
 *
 */
char* _pszFileBuffer;
int* _pnBufferPointer;
sem_t _semWrite;
sem_t _semRead;
sem_t _semMutex;
int* _pnReadCount;



/*
 *
 * Prototypes
 *
 */
void* WriterFunction(void* pvData);
void* ReaderFunction(void* pvData);
void WriteFile(const char * psz, char * pszNumber);
void ReadFile(char * pszBuffer, char * pszNumber);



/*
 *
 * Main function implementation
 *
 */
int main (int argc, char *argv[])
{

	// var init
	char szNum[TOTAL_CHILDREN / 2][10] = {"[1st]", "[2nd]", "[3rd]"};
	char cInput;
	int i;
	pid_t pidForkID[TOTAL_CHILDREN];

	// put global variables in shared memory
	_pszFileBuffer = mmap(NULL, LEN_FILE + 1, PROT_READ | PROT_WRITE, MAP_SHARED | MAP_ANONYMOUS, -1, 0);
	_pnBufferPointer = mmap(NULL, sizeof *_pnBufferPointer, PROT_READ | PROT_WRITE, MAP_SHARED | MAP_ANONYMOUS, -1, 0);
	_pnReadCount = mmap(NULL, sizeof *_pnReadCount, PROT_READ | PROT_WRITE, MAP_SHARED | MAP_ANONYMOUS, -1, 0);

	// set values for variables in shared memory
	memset(_pszFileBuffer, '\0', LEN_FILE + 1);
	_pnBufferPointer = 0;
	_pnReadCount = 0;

	// init named semaphores, this time make them multi-process so that child processes
	// 	can use them
	sem_init(&_semWrite, 1, 1);
	sem_init(&_semRead, 1, 1);
	sem_init(&_semMutex, 1, 1);

	// create processes
	for(i = 0; i < TOTAL_CHILDREN; i++)
	{
		pidForkID[2 * i] = fork();
		if (pidForkID[2 * i] == 0)
		{

			// child - writer
			WriterFunction(&szNum[i]);


			pidForkID[(2 * i) + 1] = fork();
			if (pidForkID[(2 * i) + 1] != 0)
			{

			}
			else
			{

				// child - reader
				ReaderFunction(&szNum[i]);
			}

			// exit for loop
			break;
		}
		else
		{
			// child - reader
			ReaderFunction(&szNum[i]);
		}
	}

	// wait for input
	fprintf(stdout, "\r\nPress any key to exit...");
	fflush(stdout);
	cInput = getchar();

	// destroy semaphores
	sem_close(_psemWrite);
	sem_close(_psemRead);
	sem_close(_psemMutex);

	// kill all child processes
	for(i = 0; i < (TOTAL_WRITERS * 2); i++)
	{
		kill(pidForkID[i], SIGKILL);
	}

	return 0;
}



/*
 *
 * Threading function implementations
 *
 */
void* WriterFunction(void* pvData)
{

	// var init
	char* pszNumber = (char *)pvData;
	char szData[50] = "|abcdefghijklmnopqrstuvwxyz-";

	// build write string
	strcat((char *)&szData, pszNumber);

	// write repeatedly
	while(TRUE)
	{

		// write!
		WriteFile((const char *)&szData, pszNumber);

		// sleep this thread a tick
		usleep(0);
	}

	return NULL;
}

void* ReaderFunction(void* pvData)
{

	// var init
	char* pszNumber = (char *)pvData;
	char szData[LEN_FILE + 1];

	// read repeatedly
	while(TRUE)
	{

		// clear buffer and read!
		memset(&szData, '\0', LEN_FILE + 1);
		ReadFile((char *)&szData, pszNumber);

		// sleep this thread a tick
		usleep(0);
	}

	return NULL;
}



/*
 *
 * Supporting function implementations
 *
 */
void WriteFile(const char * pszBuffer, char * pszNumber)
{

	// var init
	int i;
	int nLenPsz = strlen(pszBuffer);

	// mutex around write
	sem_wait(_psemRead);
	sem_wait(_psemWrite);

	// append to buffer and print
	fprintf(stdout, "\r\n\r\n-------------------------------\r\nSTART WRITING: %s\r\n-------------------------------\r\n", pszNumber);
	fprintf(stdout, "\r\n\r\n%s File buffer before write:\r\n%s\r\n\r\nWriting:\r\n", pszNumber, _pszFileBuffer);
	for (i = 0; i < nLenPsz; i++)
	{

		// print a character and sleep a tick
		fprintf(stdout, "%c", pszBuffer[i]);
		usleep(0);

		// append to buffer
		*_pnBufferPointer = (*_pnBufferPointer >= LEN_FILE) ? 0 : *_pnBufferPointer;
		_pszFileBuffer[*_pnBufferPointer] = pszBuffer[i];
		if ((*_pnBufferPointer + 1) < LEN_FILE)
		{
			_pszFileBuffer[*_pnBufferPointer + 1] = '@';
		}
		else
		{
			_pszFileBuffer[0] = '@';
		}
		(*_pnBufferPointer)++;
	}
	fprintf(stdout, "\r\n\r\n%s File buffer after write:\r\n%s", pszNumber, _pszFileBuffer);
	fprintf(stdout, "\r\n\r\n-------------------------------\r\nSTOP WRITING: %s\r\n-------------------------------\r\n", pszNumber);
	fflush(stdout);

	// end mutex
	sem_post(_psemWrite);
	sem_post(_psemRead);
}

void ReadFile(char * pszBuffer, char * pszNumber)
{

	// var init
	int i;
	int nLenBuffer = strlen(_pszFileBuffer);

	// mutex around read
	sem_wait(_psemRead);
	sem_wait(_psemMutex);
	(*_pnReadCount)++;
	if (*_pnReadCount == 1)
	{
		sem_wait(_psemWrite);
	}
	sem_post(_psemMutex);
	sem_post(_psemRead);

	// read data back into buffer
	fprintf(stdout, "\r\n\r\n-------------------------------\r\nSTART READING: %s\r\n-------------------------------\r\n", pszNumber);
	for (i = 0; i < nLenBuffer; i++)
	{

		// sleep a tick
		usleep(0);

		// write to read budder
		pszBuffer[i] = _pszFileBuffer[i];
	}
	fprintf(stdout, "\r\n\r\n%s File buffer read:\r\n%s", pszNumber, _pszFileBuffer);
	fprintf(stdout, "\r\n\r\n%s Returned buffer after read:\r\n%s", pszNumber, pszBuffer);
	fprintf(stdout, "\r\n\r\n-------------------------------\r\nSTOP READING: %s\r\n-------------------------------\r\n", pszNumber);
	fflush(stdout);

	// end mutex
	sem_wait(_psemMutex);
	(*_pnReadCount)--;
	if (*_pnReadCount == 0)
	{
		sem_post(_psemWrite);
	}
	sem_post(_psemMutex);
}